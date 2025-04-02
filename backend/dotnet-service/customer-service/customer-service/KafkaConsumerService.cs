using Confluent.Kafka;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerService;

public class KafkaConsumerService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<KafkaConsumerService> _logger;

    public KafkaConsumerService(IServiceProvider serviceProvider, ILogger<KafkaConsumerService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("KafkaConsumerService is starting...");

        var config = new ConsumerConfig
        {
            BootstrapServers = Environment.GetEnvironmentVariable("KAFKA_BROKER"),
            GroupId = "customer-group",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = true,
            SocketTimeoutMs = 5000,
            SessionTimeoutMs = 6000,
            MetadataMaxAgeMs = 5000,
            EnablePartitionEof = true
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

        try
        {
            consumer.Subscribe("user-registered");
            _logger.LogInformation("Subscribed to topic 'user-registered'");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to subscribe to Kafka topic.");
            return;
        }

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var result = consumer.Consume(TimeSpan.FromMilliseconds(500));
                    if (result == null || string.IsNullOrEmpty(result.Message?.Value))
                    {
                        continue;
                    }

                    string rawJson = result.Message.Value;
                    _logger.LogDebug("Received Kafka message: {Message}", rawJson);

                    Dictionary<string, string>? data = null;
                    try
                    {
                        data = JsonSerializer.Deserialize<Dictionary<string, string>>(rawJson);
                    }
                    catch (JsonException jex)
                    {
                        _logger.LogWarning(jex, "Invalid JSON format received from Kafka.");
                        continue;
                    }

                    if (data == null || !data.TryGetValue("email", out var email) || string.IsNullOrWhiteSpace(email))
                    {
                        _logger.LogWarning("Kafka message missing 'email' field.");
                        continue;
                    }

                    data.TryGetValue("name", out var name); // Optional field

                    using var scope = _serviceProvider.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<CustomerDbContext>();

                    var exists = await db.Customers.FirstOrDefaultAsync(c => c.Email == email, stoppingToken);
                    if (exists == null)
                    {
                        db.Customers.Add(new Customer
                        {
                            Email = email,
                            Name = name
                        });

                        await db.SaveChangesAsync(stoppingToken);
                        _logger.LogInformation("✅ Added customer: {Email}", email);
                    }
                    else
                    {
                        _logger.LogInformation("Customer already exists: {Email}", email);
                    }
                }
                catch (ConsumeException cex)
                {
                    _logger.LogError(cex, "Kafka consume error.");
                }
                catch (OperationCanceledException)
                {
                    _logger.LogWarning("KafkaConsumerService cancelled during polling.");
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unhandled error during message processing.");
                }
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("KafkaConsumerService is shutting down.");
        }
        finally
        {
            consumer.Close();
            _logger.LogInformation("KafkaConsumerService has closed the consumer.");
        }
    }
}

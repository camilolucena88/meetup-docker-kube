using Confluent.Kafka;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerService;

public class KafkaConsumerService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public KafkaConsumerService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = Environment.GetEnvironmentVariable("KAFKA_BROKER"),
            GroupId = "customer-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe("user-registered");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var cr = consumer.Consume(stoppingToken);
                var data = JsonSerializer.Deserialize<Dictionary<string, string>>(cr.Message.Value);
                var email = data?["email"];

                if (!string.IsNullOrEmpty(email))
                {
                    using var scope = _serviceProvider.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<CustomerDbContext>();

                    var exists = await db.Customers.FirstOrDefaultAsync(c => c.Email == email);
                    if (exists == null)
                    {
                        db.Customers.Add(new Customer { Email = email });
                        await db.SaveChangesAsync();
                        Console.WriteLine($"✅ Added customer: {email}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Kafka Error: {ex.Message}");
            }
        }
    }
}
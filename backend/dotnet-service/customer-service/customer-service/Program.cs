using Microsoft.EntityFrameworkCore;
using CustomerService;
using Microsoft.Extensions.Hosting;
using Prometheus;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CustomerDbContext>(options =>
    options.UseNpgsql(Environment.GetEnvironmentVariable("DATABASE_URL")));

builder.Services.AddHostedService<KafkaConsumerService>();

var app = builder.Build();
app.UseMetricServer();
app.UseHttpMetrics();
app.MapGet("/health", () => Results.Ok(new { status = "Customer Service is running" }));
app.MapGet("/customers", async (CustomerDbContext db) => await db.Customers.ToListAsync());

// 👉 Auto-apply migrations at startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CustomerDbContext>();
    db.Database.Migrate();
}

app.Run();
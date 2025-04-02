using Microsoft.EntityFrameworkCore;
using CustomerService;
using Microsoft.Extensions.Hosting;
using Prometheus;

try {
    DotNetEnv.Env.Load();

    var builder = WebApplication.CreateBuilder(args);

    // Optional: force binding
    builder.WebHost.UseUrls("http://0.0.0.0:5288");

    builder.Services.AddDbContext<CustomerDbContext>(options =>
        options.UseNpgsql(Environment.GetEnvironmentVariable("DATABASE_URL")));

    builder.Services.AddHostedService<KafkaConsumerService>();

    var app = builder.Build();
    app.UseMetricServer();
    app.UseHttpMetrics();
    app.MapGet("/health", () => Results.Ok(new { status = "Customer Service is running" }));
    app.MapGet("/customers", async (CustomerDbContext db) => await db.Customers.ToListAsync());
    app.MapGet("/", () => Results.Ok(new { status = "Customer Service root is running" }));
    // 👉 Auto-apply migrations at startup
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<CustomerDbContext>();
        db.Database.Migrate();
    }
    app.Run();
}
catch (Exception ex) {
    Console.WriteLine($"FATAL ERROR: {ex}");
    throw;
}
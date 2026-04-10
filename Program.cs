using Azure.Monitor.OpenTelemetry.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddOpenTelemetry().UseAzureMonitor(options =>
{
    options.ConnectionString =
        builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"] ??
        builder.Configuration["ApplicationInsights:ConnectionString"];
});

builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Information);

var app = builder.Build();
var logger = app.Logger;

logger.LogInformation("Ajay Managaon Application starting...");

logger.LogInformation("Application built successfully");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    logger.LogInformation("Running in Production environment");

    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseRouting();

logger.LogInformation("Middleware configured");

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

logger.LogInformation("Application configured. Starting app...");

app.Run();

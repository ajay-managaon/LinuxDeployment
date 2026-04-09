var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddApplicationInsightsTelemetry();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddApplicationInsights();
builder.Logging.SetMinimumLevel(LogLevel.Information);

// Create logger
var logger = LoggerFactory
    .Create(logging =>
    {
        logging.AddConsole();
    })
    .CreateLogger("Program");

logger.LogInformation("Ajay Managaon Application starting...");

var app = builder.Build();

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
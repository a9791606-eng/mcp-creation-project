using McpEventPlanner.Services;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// קרא configuration מ-.env ומ-appsettings
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// הוסף את ה-Orchestrator
builder.Services.AddSingleton<EventPlanningOrchestrator>();

// הוסף Rate Limiting
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("GeneratePlan", policy =>
    {
        policy.PermitLimit = 10;
        policy.Window = TimeSpan.FromMinutes(1);
        policy.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        policy.QueueLimit = 2;
    });

    options.AddFixedWindowLimiter("Health", policy =>
    {
        policy.PermitLimit = 100;
        policy.Window = TimeSpan.FromMinutes(1);
    });

    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        await context.HttpContext.Response.WriteAsJsonAsync(new 
        { 
            error = "הגעת ללימיט של בקשות. נסה שוב בעוד דקה.",
            retryAfter = context.HttpContext.Response.Headers["Retry-After"]
        }, cancellationToken: token);
    };
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRateLimiter();
app.MapControllers();
app.Run();

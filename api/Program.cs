using System.Diagnostics;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using api.Options;
using api.Extensions;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory(),
});

// Configure logging.
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Information);

// Load configuration.
if (Debugger.IsAttached)
{
    builder.Configuration.AddJsonFile(@"appsettings.debug.json", optional: true, reloadOnChange: true);
}

builder.Configuration.AddJsonFile($@"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                     .AddJsonFile($@"appsettings.{Environment.UserName}.json", optional: true, reloadOnChange: true)
                     .AddEnvironmentVariables();

// Services.
builder.Services.AddHealthChecks();

builder.Services.AddEndpointsApiExplorer()
                .AddSwaggerGen();

builder.Services.AddApplicationDbContext(builder.Configuration.GetConnectionString("SQLServer"));

builder.Services.AddCorsOptions();

builder.Services.AddCookieConfiguration();

builder.Services.AddHttpContextAccessor();

builder.Services.AddCustomIdentity()
                .AddAuthenticationServices();

// Swagger configuration and controllers.
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(Constants.AppVersion, new OpenApiInfo { Title = Constants.AppTitle, Version = Constants.AppVersion });
    c.EnableAnnotations();
});

builder.Services.AddControllers();

// Build the app.
var app = builder.Build();

// Get logger.
var logger = app.Services.GetRequiredService<ILogger<Program>>();

// Log environment userName.
logger.LogInformation("Environment UserName: {UserName}", Environment.UserName);

if (app.Environment.IsDevelopment())
{
    builder.Logging.AddConsole();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint(Constants.SwaggerEndpoint, $@"{Constants.AppTitle} {Constants.AppVersion}");
    });
}

app.UseHttpsRedirection()
   .UseCors("AllowReactApp")
   .UseAuthorization()
   .UseStatusCodePages();

app.MapControllers();

app.Run();

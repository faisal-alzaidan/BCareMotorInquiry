using Microsoft.AspNetCore.Localization;
using Motor.Inquiry.API.Extensions;
using Motor.Inquiry.API.Middlewares;
using Motor.Inquiry.Infrastructure.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// 1. Serilog Configuration
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();


// 2. Add Controllers & API Documentations
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

// 3. Register Modular Extensions (Clean & Enterprise Level)
builder.Services.AddDatabaseExtensions(builder.Configuration);
builder.Services.AddValidationAndMapping();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddApiLocalization();
builder.Services.AddHealthChecks()
    .AddDbContextCheck<AppDbContext>("Database");

builder.Services.AddAppRateLimiter();

var app = builder.Build();


// 4. Configure the HTTP request pipeline
app.UseApiLocalization();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers()
   .RequireRateLimiting("fixed");

app.MapHealthChecks("/health");

app.Run();
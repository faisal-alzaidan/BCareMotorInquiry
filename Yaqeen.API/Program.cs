using Yaqeen.API.Extensions;
using Yaqeen.API.Middlewares;
using Yaqeen.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// 1. Add Controllers & API Documentations
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// 2. Register Modular Extensions
builder.Services.AddYaqeenDatabase(builder.Configuration);
builder.Services.AddYaqeenValidation();
builder.Services.AddYaqeenApplicationServices();

builder.Services.AddAppRateLimiter();

builder.Services.AddHealthChecks().AddDbContextCheck<YaqeenDbContext>("Database");

var app = builder.Build();

// 3. Configure the HTTP request pipeline
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseRateLimiter();

app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers()
   .RequireRateLimiting("fixed");

app.MapHealthChecks("/health");

app.Run();
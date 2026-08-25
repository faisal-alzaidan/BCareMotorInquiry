using Yaqeen.API.Extensions;
using Yaqeen.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// 1. Add Controllers & API Documentations
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// 2. Register Modular Extensions
builder.Services.AddYaqeenDatabase(builder.Configuration);
builder.Services.AddYaqeenValidation();
builder.Services.AddYaqeenApplicationServices();

var app = builder.Build();

// 3. Configure the HTTP request pipeline
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
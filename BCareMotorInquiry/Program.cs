using Microsoft.EntityFrameworkCore;
using Yaqeen.Application.Interfaces;
using Yaqeen.Application.Services;
using Yaqeen.Infrastructure.Persistence;
using Yaqeen.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();


app.Run();

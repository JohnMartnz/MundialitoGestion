using Microsoft.EntityFrameworkCore;
using Mundialito.Api.Endpoints;
using Mundialito.Application.Common.Interfaces;
using Mundialito.Application.Equipos.Commands.CrearEquipo;
using Mundialito.Infrastructure.Persistence;
using Mundialito.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<MundialitoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();

// UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Repositories
builder.Services.AddScoped<IEquipoRepository, EquipoRepository>();

// MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CrearEquipoCommand).Assembly);
});

var app = builder.Build();

app.MapEquiposEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

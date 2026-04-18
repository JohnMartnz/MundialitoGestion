using Microsoft.EntityFrameworkCore;
using Mundialito.Api.Endpoints;
using Mundialito.Api.Middleware;
using Mundialito.Application.Common.Interfaces;
using Mundialito.Application.Equipos.Commands.CrearEquipo;
using Mundialito.Application.Jugadores.Commands.CrearJugador;
using Mundialito.Infrastructure.Idempotency;
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
builder.Services.AddScoped<IJugadorRepository, JugadorRepository>();
builder.Services.AddScoped<IPartidoRepository, PartidoRepository>();
builder.Services.AddScoped<IIdempotencyRepository, IdempotencyRepository>();

// MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CrearEquipoCommand).Assembly);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseMiddleware<IdempotencyMiddleware>();

app.UseAuthorization();

app.MapEquiposEndpoints();
app.MapJugadoresEndpoints();
app.MapPartidosEndpoints();

app.MapControllers();

app.Run();

using Microsoft.EntityFrameworkCore;
using Mundialito.Application.Common.Interfaces;
using Mundialito.Domain.Entities;
using Mundialito.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Infrastructure.Repositories
{
    public class JugadorRepository : IJugadorRepository
    {
        private readonly MundialitoDbContext _context;

        public JugadorRepository(MundialitoDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Jugador jugador, CancellationToken cancellationToken = default)
        {
            await _context.Jugadores.AddAsync(jugador, cancellationToken);
        }

        public Task<bool> EquipoExistsAsync(Guid equipoId, CancellationToken cancellationToken = default)
        {
            return _context.Equipos.AnyAsync(equipo => equipo.Id == equipoId, cancellationToken);
        }

        public async Task<bool> IsEquipoAvailableToAddJugadores(Guid equipoId, CancellationToken cancellationToken = default)
        {
            const int maxJugadoresPorEquipo = 15;
            var jugadoresCount = await _context.Jugadores.CountAsync(jugador => jugador.EquipoId == equipoId, cancellationToken);
            return jugadoresCount < maxJugadoresPorEquipo;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Mundialito.Application.Common.Interfaces;
using Mundialito.Domain.Entities;
using Mundialito.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Infrastructure.Repositories
{
    public class PartidoRepository : IPartidoRepository
    {
        private readonly MundialitoDbContext _context;

        public PartidoRepository(MundialitoDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Partido partido, CancellationToken cancellationToken = default)
        {
            await _context.Partidos.AddAsync(partido, cancellationToken);
        }

        public async Task UpdateAsync(Partido partido, CancellationToken cancellationToken = default)
        {
            _context.Partidos.Update(partido);
            await Task.CompletedTask;
        }

        public async Task<bool> EquipoExistsAsync(Guid equipoId, CancellationToken cancellationToken = default)
        {
            return await _context.Equipos.AnyAsync(equipo => equipo.Id == equipoId, cancellationToken);
        }

        public async Task<Partido?> GetByIdAsync(Guid partidoId, CancellationToken cancellationToken = default)
        {
            return await _context.Partidos.Include(p => p.EquipoLocal)
                .Include(p => p.EquipoVisitante)
                .FirstOrDefaultAsync(p => p.Id == partidoId, cancellationToken);
        }
    }
}

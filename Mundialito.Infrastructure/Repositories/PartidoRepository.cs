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

        public async Task<bool> EquipoExistsAsync(Guid equipoId, CancellationToken cancellationToken = default)
        {
            return await _context.Equipos.AnyAsync(equipo => equipo.Id == equipoId, cancellationToken);
        }
    }
}

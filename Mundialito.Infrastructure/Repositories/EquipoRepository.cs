using Microsoft.EntityFrameworkCore;
using Mundialito.Application.Common.Interfaces;
using Mundialito.Domain.Entities;
using Mundialito.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Infrastructure.Repositories
{
    public class EquipoRepository : IEquipoRepository
    {
        private readonly MundialitoDbContext _context;

        public EquipoRepository(MundialitoDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Equipo equipo, CancellationToken cancellationToken = default)
        {
            await _context.Equipos.AddAsync(equipo, cancellationToken);
        }

        public async Task<bool> ExisteNombreAsync(string nombre, CancellationToken cancellationToken = default)
        {
            return await _context.Equipos.AnyAsync(equipo => equipo.Nombre == nombre, cancellationToken);
        }
    }
}

using Mundialito.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Application.Common.Interfaces
{
    public interface IPartidoRepository
    {
        Task AddAsync(Partido partido, CancellationToken cancellationToken = default);
        Task UpdateAsync(Partido partido, CancellationToken cancellationToken = default);
        Task<bool> EquipoExistsAsync(Guid equipoId, CancellationToken cancellationToken = default);
        Task<Partido?> GetByIdAsync(Guid partidoId, CancellationToken cancellationToken = default);
    }
}

using Mundialito.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Application.Common.Interfaces
{
    public interface IJugadorRepository
    {
        Task AddAsync(Jugador jugador, CancellationToken cancellationToken = default);
        Task<bool> EquipoExistsAsync(Guid equipoId, CancellationToken cancellationToken = default);
        Task<bool> IsEquipoAvailableToAddJugadores(Guid equipoId, CancellationToken cancellationToken = default);
    }
}

using Mundialito.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Application.Common.Interfaces
{
    public interface IEquipoRepository
    {
        Task AddAsync(Equipo equipo, CancellationToken cancellationToken = default);
        Task<bool> ExisteNombreAsync(string nombre, CancellationToken cancellationToken = default);
    }
}

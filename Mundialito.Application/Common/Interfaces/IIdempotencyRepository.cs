using Mundialito.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Application.Common.Interfaces
{
    public interface IIdempotencyRepository
    {
        Task<IdempotencyRequest?> GetByKeyAsync(Guid key, CancellationToken cancellationToken);
        Task AddAsync(IdempotencyRequest request, CancellationToken cancellationToken = default);
    }
}

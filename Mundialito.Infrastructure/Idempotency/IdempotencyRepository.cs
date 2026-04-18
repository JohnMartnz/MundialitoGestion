using Microsoft.EntityFrameworkCore;
using Mundialito.Application.Common.Interfaces;
using Mundialito.Domain.Entities;
using Mundialito.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Infrastructure.Idempotency
{
    public class IdempotencyRepository : IIdempotencyRepository
    {
        private readonly MundialitoDbContext _context;

        public IdempotencyRepository(MundialitoDbContext context)
        {
            _context = context;
        }

        public async Task<IdempotencyRequest?> GetByKeyAsync(Guid key, CancellationToken cancellationToken = default)
        {
            return await _context.IdempotencyRequests.FirstOrDefaultAsync(x => x.Key == key, cancellationToken);
        }

        public async Task AddAsync(IdempotencyRequest request, CancellationToken cancellation = default)
        {
            await _context.IdempotencyRequests.AddAsync(request, cancellation);
            await _context.SaveChangesAsync(cancellation);
        }
    }
}

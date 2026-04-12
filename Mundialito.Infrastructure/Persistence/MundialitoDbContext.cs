using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Infrastructure.Persistence
{
    internal class MundialitoDbContext : DbContext
    {
        public MundialitoDbContext(DbContextOptions<MundialitoDbContext> options) : base(options)
        {
        }
    }
}

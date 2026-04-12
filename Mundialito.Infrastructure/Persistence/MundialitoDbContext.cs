using Microsoft.EntityFrameworkCore;
using Mundialito.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Infrastructure.Persistence
{
    public class MundialitoDbContext : DbContext
    {
        public MundialitoDbContext(DbContextOptions<MundialitoDbContext> options) : base(options)
        {
        }

        public DbSet<Equipo> Equipos => Set<Equipo>();
        public DbSet<Jugador> Jugadores => Set<Jugador>();
        public DbSet<Partido> Partidos => Set<Partido>();
        public DbSet<IdempotencyRequest> IdempotencyRequests => Set<IdempotencyRequest>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Partido>(entity =>
            {
                entity.HasOne( p => p.EquipoLocal)
                .WithMany()
                .HasForeignKey( p => p.EquipoLocalId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.EquipoVisitante)
                .WithMany()
                .HasForeignKey(p => p.EquipoVisitanteId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MundialitoDbContext).Assembly);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultaCreditoService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsultaCreditoService.Infrastructure.Database;
public sealed class ConsultaCreditoServiceDbContext(DbContextOptions<ConsultaCreditoServiceDbContext> options)
    : DbContext(options)
{
    public DbSet<Credito> Creditos { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConsultaCreditoServiceDbContext).Assembly);

        modelBuilder.HasDefaultSchema(Schemas.Default);
    }
}

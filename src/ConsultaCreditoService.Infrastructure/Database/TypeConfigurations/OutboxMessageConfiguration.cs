using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultaCreditoService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsultaCreditoService.Infrastructure.Database.TypeConfigurations;
public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("outbox");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasColumnName("id");

        builder.Property(s => s.Topic)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("topic");

        builder.Property(s => s.Payload)
            .IsRequired()
            .HasMaxLength(350)
            .HasColumnName("payload");

        builder.Property(s => s.CreatedAt)
           .HasColumnName("created_at");

        builder.Property(s => s.ProcessedAt)
           .HasColumnName("processed_at");
    }
}

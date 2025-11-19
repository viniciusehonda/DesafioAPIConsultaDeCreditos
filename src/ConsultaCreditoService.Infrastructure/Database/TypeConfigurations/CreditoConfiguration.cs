using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultaCreditoService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsultaCreditoService.Infrastructure.Database;
public class CreditoConfiguration : IEntityTypeConfiguration<Credito>
{
    public void Configure(EntityTypeBuilder<Credito> builder)
    {
        builder.ToTable("credito");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasColumnName("id");

        builder.Property(s => s.NumeroCredito)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("numero_credito");

        builder.Property(s => s.NumeroNfse)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("numero_nfse");

        builder.Property(s => s.DataConstituicao)
            .IsRequired()
            .HasColumnName("data_constituicao");

        builder.Property(s => s.ValorIssqn)
            .IsRequired()
            .HasPrecision(15, 2)
            .HasColumnName("valor_issqn");

        builder.Property(s => s.TipoCredito)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("tipo_credito");

        builder.Property(s => s.IsSimplesNacional)
            .IsRequired()
            .HasColumnName("simples_nacional");

        builder.Property(s => s.Aliquota)
            .IsRequired()
            .HasPrecision(5, 2)
            .HasColumnName("aliquota");

        builder.Property(s => s.ValorFaturado)
            .IsRequired()
            .HasPrecision(15, 2)
            .HasColumnName("valor_faturado");

        builder.Property(s => s.ValorDeducao)
            .IsRequired()
            .HasPrecision(15, 2)
            .HasColumnName("valor_deducao");

        builder.Property(s => s.BaseCalculo)
            .IsRequired()
            .HasPrecision(15, 2)
            .HasColumnName("base_calculo");
    }
}

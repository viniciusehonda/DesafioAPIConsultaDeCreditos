using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultaCreditoService.Application.Credito.ConsultarCreditoPorNfse;
using ConsultaCreditoService.Domain.Entities;
using ConsultaCreditoService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ConsultaCreditoService.UnitTests.Application;
public class ConsultarCreditoPorNfseQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnMappedResultsForMatchingNfse()
    {
        // Arrange
        ConsultaCreditoServiceDbContext db = Mocks.Mocks.CreateDbContext();

        db.Creditos.AddRange(
            new Credito(1, "1231", "NFSE123", new DateTime(2024, 02, 25, 0, 0, 0, DateTimeKind.Utc),
                100m, "Normal", true, 5m, 1000m, 10m, 990m),
            new Credito(2, "31313", "NFSE999", new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                200m, "Especial", false, 2m, 2000m, 50m, 1950m)
        );
        await db.SaveChangesAsync();

        var handler = new ConsultarCreditoPorNfseQueryHandler(db);

        var query = new ConsultarCreditoPorNfseQuery("NFSE123");

        // Act
        SharedKernel.Result<List<ConsultarCreditoPorNfseResponse>> result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Single(result.Value);

        ConsultarCreditoPorNfseResponse item = result.Value.FirstOrDefault();

        Assert.NotNull(item);
        Assert.Equal("1231", item.NumeroCredito);
        Assert.Equal("NFSE123", item.NumeroNfse);
        Assert.Equal("2024-02-25", item.DataConstituicao);
        Assert.Equal(100m, item.ValorIssqn);
        Assert.Equal("Normal", item.TipoCredito);
        Assert.Equal("Sim", item.SimplesNacional);
        Assert.Equal(5m, item.Aliquota);
        Assert.Equal(1000m, item.ValorFaturado);
        Assert.Equal(10m, item.ValorDeducao);
        Assert.Equal(990m, item.BaseCalculo);

        await db.DisposeAsync();
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoMatchFound()
    {
        // Arrange
        ConsultaCreditoServiceDbContext db = Mocks.Mocks.CreateDbContext();

        db.Creditos.Add(new Credito(1, "CR1", "OTHER", DateTime.UtcNow,
            100m, "Normal", true, 5m, 1000m, 10m, 990m));
        await db.SaveChangesAsync();

        var handler = new ConsultarCreditoPorNfseQueryHandler(db);

        var query = new ConsultarCreditoPorNfseQuery("NFSE-NOT-EXIST");

        // Act
        SharedKernel.Result<List<ConsultarCreditoPorNfseResponse>> result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Value);

        await db.DisposeAsync();
    }
}

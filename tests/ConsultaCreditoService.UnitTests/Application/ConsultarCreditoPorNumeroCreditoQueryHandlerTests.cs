using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultaCreditoService.Application.Credito.ConsultarCreditoPorNumeroCredito;
using ConsultaCreditoService.Domain.Entities;

namespace ConsultaCreditoService.UnitTests.Application;
public class ConsultarCreditoPorNumeroCreditoQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnCredito_WhenNumeroCreditoExists()
    {
        // Arrange
        Infrastructure.Database.ConsultaCreditoServiceDbContext db = Mocks.Mocks.CreateDbContext();
        var credito = new Credito(1, "CRED-123", "NF-001", new DateTime(2024, 10, 18, 0, 0, 0, DateTimeKind.Utc),
                120.75m, "Normal", true, 5m, 1000m, 10m, 990m);

        db.Add(credito);
        await db.SaveChangesAsync();

        var handler = new ConsultarCreditoPorNumeroCreditoQueryHandler(db);

        var query = new ConsultarCreditoPorNumeroCreditoQuery("CRED-123");

        // Act
        SharedKernel.Result<ConsultarCreditoPorNumeroCreditoResponse> result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal("CRED-123", result.Value.NumeroCredito);
        Assert.Equal("NF-001", result.Value.NumeroNfse);
        Assert.Equal("2024-10-18", result.Value.DataConstituicao);
        Assert.Equal(120.75m, result.Value.ValorIssqn);
        Assert.Equal("Normal", result.Value.TipoCredito);
        Assert.Equal("Sim", result.Value.SimplesNacional);

        await db.DisposeAsync();
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenNumeroCreditoDoesNotExist()
    {
        // Arrange
        Infrastructure.Database.ConsultaCreditoServiceDbContext db = Mocks.Mocks.CreateDbContext();
        var handler = new ConsultarCreditoPorNumeroCreditoQueryHandler(db);

        var query = new ConsultarCreditoPorNumeroCreditoQuery("NOT_FOUND");

        // Act
        SharedKernel.Result<ConsultarCreditoPorNumeroCreditoResponse> result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Error);

        await db.DisposeAsync();
    }
}

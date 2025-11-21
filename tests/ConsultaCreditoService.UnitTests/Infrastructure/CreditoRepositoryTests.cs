using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultaCreditoService.Domain.Entities;
using ConsultaCreditoService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ConsultaCreditoService.UnitTests.Infrastructure;
public class CreditoRepositoryTests
{
    [Fact]
    public async Task AddCreditos_ShouldAddAllItems_AndSaveChanges()
    {
        // Arrange
        ConsultaCreditoService.Infrastructure.Database.ConsultaCreditoServiceDbContext dbContext = Mocks.Mocks.CreateDbContext();

        var repository = new CreditoRepository(dbContext);

        var creditos = new List<Credito>
        {
            new Credito(
                id: 1,
                numeroCredito: "123",
                numeroNfse: "NFSE001",
                dataConstituicao: DateTime.UtcNow,
                valorIssqn: 10,
                tipoCredito: "Servico",
                isSimplesNacional: false,
                aliquota: 5,
                valorFaturado: 100,
                valorDeducao: 0,
                baseCalculo: 100),

            new Credito(
                id: 2,
                numeroCredito: "456",
                numeroNfse: "NFSE002",
                dataConstituicao: DateTime.UtcNow,
                valorIssqn: 20,
                tipoCredito: "Servico",
                isSimplesNacional: true,
                aliquota: 3,
                valorFaturado: 200,
                valorDeducao: 0,
                baseCalculo: 200)
        };

        // Act
        await repository.AddCreditos(creditos, CancellationToken.None);

        // Assert
        List<Credito> savedCreditos = await dbContext.Set<Credito>().ToListAsync();

        Assert.Equal(2, savedCreditos.Count);

        Assert.Contains(savedCreditos, c => c.NumeroCredito == "123");
        Assert.Contains(savedCreditos, c => c.NumeroCredito == "456");

        await dbContext.DisposeAsync();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultaCreditoService.Application.Credito.AdicionarCreditoConstituido;
using ConsultaCreditoService.Domain.Entities;
using ConsultaCreditoService.Domain.Repository;
using Moq;

namespace ConsultaCreditoService.UnitTests.Application;
public class AdicionarCreditoConstituidoCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCallRepositoryOnce_AndReturnSuccess()
    {
        // Arrange
        var repoMock = new Mock<ICreditoRepository>();

        var items = new List<Credito>();

        var command = new AdicionarCreditoConstituidoCommand(items);

        repoMock
            .Setup(r => r.AddCreditos(It.IsAny<List<Credito>>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new AdicionarCreditoConstituidoCommandHandler(repoMock.Object);

        // Act
        SharedKernel.Result result = await handler.Handle(command, CancellationToken.None);

        // Assert
        repoMock.Verify(r => r.AddCreditos(items, It.IsAny<CancellationToken>()), Times.Once);

        Assert.True(result.IsSuccess);
    }
}

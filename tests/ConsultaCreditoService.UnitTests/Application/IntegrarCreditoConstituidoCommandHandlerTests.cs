using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultaCreditoService.Application.Credito.IntegrarCreditoConstituido;
using ConsultaCreditoService.Domain.Entities;
using ConsultaCreditoService.Infrastructure.Messaging;
using Moq;

namespace ConsultaCreditoService.UnitTests.Application;
public class IntegrarCreditoConstituidoCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldPublishCreditos_AndReturnSuccessResponse()
    {
        // Arrange
        var publisherMock = new Mock<IAzureServiceBusPublisher>();

        var handler = new IntegrarCreditoConstituidoCommandHandler(publisherMock.Object);

        var items = new List<IntegrarCreditoConstituidoDto>
        {
            new("CRED-001", "NF-123", "2024-10-20", 100.50m, "Normal", "Sim", 5m, 1000m, 50m, 950m),
            new("CRED-002", "NF-456", "2024-10-21", 200.00m, "Especial", "Não", 3m, 2000m, 200m, 1800m)
        };

        var command = new IntegrarCreditoConstituidoCommand(items);

        // Act
        SharedKernel.Result<IntegrarCreditoConstituidoResponse> result = await handler.Handle(command, CancellationToken.None);

        // Assert: handler result
        Assert.True(result.IsSuccess);
        Assert.True(result.Value.Success);

        // Assert: PublishAsync called once with correct topic
        publisherMock.Verify(p =>
            p.PublishAsync(
                CreditoMessageTopics.IntegrarCreditoConstituidoEntry,
                It.Is<IEnumerable<Credito>>(creditos =>
                    creditos.Count() == 2 &&
                    creditos.Any(c => c.NumeroCredito == "CRED-001") &&
                    creditos.Any(c => c.NumeroCredito == "CRED-002")
                )
            ),
            Times.Once
        );
    }

    [Fact]
    public async Task Handle_ShouldPublishEmptyList_WhenCommandHasNoItems()
    {
        // Arrange
        var publisherMock = new Mock<IAzureServiceBusPublisher>();
        var handler = new IntegrarCreditoConstituidoCommandHandler(publisherMock.Object);

        var command = new IntegrarCreditoConstituidoCommand(new List<IntegrarCreditoConstituidoDto>());

        // Act
        SharedKernel.Result<IntegrarCreditoConstituidoResponse> result = await handler.Handle(command, CancellationToken.None);

        // Assert return
        Assert.True(result.IsSuccess);
        Assert.True(result.Value.Success);

        // Assert: PublishAsync called with empty IEnumerable
        publisherMock.Verify(p =>
            p.PublishAsync(
                CreditoMessageTopics.IntegrarCreditoConstituidoEntry,
                It.Is<IEnumerable<Credito>>(creditos => !creditos.Any())
            ),
            Times.Once
        );
    }
}

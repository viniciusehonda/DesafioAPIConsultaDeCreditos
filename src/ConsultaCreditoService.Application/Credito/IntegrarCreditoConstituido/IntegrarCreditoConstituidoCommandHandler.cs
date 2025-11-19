using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultaCreditoService.Application.Abstractions.Messaging;
using ConsultaCreditoService.Domain.Repository;
using ConsultaCreditoService.Infrastructure.Database;
using ConsultaCreditoService.Infrastructure.Messaging;
using SharedKernel;

namespace ConsultaCreditoService.Application.Credito.IntegrarCreditoConstituido;
internal sealed class IntegrarCreditoConstituidoCommandHandler(AzureServiceBusPublisher serviceBusPublisher)
    : ICommandHandler<IntegrarCreditoConstituidoCommand>
{
    public async Task<Result> Handle(IntegrarCreditoConstituidoCommand command, CancellationToken cancellationToken)
    {
        var credito = Domain.Entities.Credito.Create(
            command.NumeroCredito,
            command.NumeroNfse,
            command.DataConstituicao,
            command.ValorIssqn,
            command.TipoCredito,
            command.SimplesNacional,
            command.Aliquota,
            command.ValorFaturado,
            command.ValorDeducao,
            command.BaseCalculo
            );

        await serviceBusPublisher.PublishAsync(Domain.Entities.CreditoMessageTopics.IntegrarCreditoConstituidoEntry, credito);

        return Result.Success();
    }
}

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
        IEnumerable<Domain.Entities.Credito> creditos = command.Items
            .Select(c => Domain.Entities.Credito.Create(
                c.NumeroCredito,
                c.NumeroNfse,
                c.DataConstituicao,
                c.ValorIssqn,
                c.TipoCredito,
                c.SimplesNacional,
                c.Aliquota,
                c.ValorFaturado,
                c.ValorDeducao,
                c.BaseCalculo
            ));

        await serviceBusPublisher.PublishAsync(Domain.Entities.CreditoMessageTopics.IntegrarCreditoConstituidoEntry, creditos);

        return Result.Success();
    }
}

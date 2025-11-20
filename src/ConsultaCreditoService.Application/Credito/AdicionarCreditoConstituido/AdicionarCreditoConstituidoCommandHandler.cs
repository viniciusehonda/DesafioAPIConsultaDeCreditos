using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultaCreditoService.Application.Abstractions.Messaging;
using ConsultaCreditoService.Domain.Entities;
using ConsultaCreditoService.Domain.Repository;
using SharedKernel;

namespace ConsultaCreditoService.Application.Credito.AdicionarCreditoConstituido;
internal sealed class AdicionarCreditoConstituidoCommandHandler(ICreditoRepository repository)
    : ICommandHandler<AdicionarCreditoConstituidoCommand>
{
    public async Task<Result> Handle(AdicionarCreditoConstituidoCommand command, CancellationToken cancellationToken)
    {
        await repository.AddCreditos(command.Items, cancellationToken);

        return Result.Success();
    }
}

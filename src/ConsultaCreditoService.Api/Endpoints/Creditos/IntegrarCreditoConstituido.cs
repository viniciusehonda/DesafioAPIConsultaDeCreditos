
using ConsultaCreditoService.Api.Extensions;
using ConsultaCreditoService.Api.Infrastructure;
using ConsultaCreditoService.Application.Abstractions.Messaging;
using ConsultaCreditoService.Application.Credito.IntegrarCreditoConstituido;
using SharedKernel;

namespace ConsultaCreditoService.Api.Endpoints.Creditos;

internal sealed class IntegrarCreditoConstituido : IEndpoint
{
    public sealed record Request(string NumeroCredito,
        string NumeroNfse,
        DateTime DataConstituicao,
        decimal ValorIssqn,
        string TipoCredito,
        bool SimplesNacional,
        decimal Aliquota,
        decimal ValorFaturado,
        decimal ValorDeducao,
        decimal BaseCalculo);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/creditos/integrar-credito-constituido", async (
            Request request,
            ICommandHandler<IntegrarCreditoConstituidoCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new IntegrarCreditoConstituidoCommand(
                request.NumeroCredito,
                request.NumeroNfse,
                request.DataConstituicao,
                request.ValorIssqn,
                request.TipoCredito,
                request.SimplesNacional,
                request.Aliquota,
                request.ValorFaturado,
                request.ValorDeducao,
                request.BaseCalculo);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(() => Results.Ok(), CustomResults.Problem);
        })
        .WithTags("Creditos");
    }
}

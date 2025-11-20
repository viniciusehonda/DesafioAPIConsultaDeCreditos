
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
        string DataConstituicao,
        decimal ValorIssqn,
        string TipoCredito,
        string SimplesNacional,
        decimal Aliquota,
        decimal ValorFaturado,
        decimal ValorDeducao,
        decimal BaseCalculo);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/creditos/integrar-credito-constituido", async (
            List<Request> requestItems,
            ICommandHandler<IntegrarCreditoConstituidoCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new IntegrarCreditoConstituidoCommand([.. requestItems
                .Select(r =>
                    new IntegrarCreditoConstituidoDto(
                        r.NumeroCredito,
                        r.NumeroNfse,
                        r.DataConstituicao,
                        r.ValorIssqn,
                        r.TipoCredito,
                        r.SimplesNacional,
                        r.Aliquota,
                        r.ValorFaturado,
                        r.ValorDeducao,
                        r.BaseCalculo)
            )]);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(() => Results.Ok(), CustomResults.Problem);
        })
        .WithTags("Creditos");
    }
}

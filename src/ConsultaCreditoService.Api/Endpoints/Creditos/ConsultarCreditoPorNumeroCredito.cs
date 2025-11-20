
using ConsultaCreditoService.Api.Extensions;
using ConsultaCreditoService.Api.Infrastructure;
using ConsultaCreditoService.Application.Abstractions.Messaging;
using ConsultaCreditoService.Application.Credito.ConsultarCreditoPorNfse;
using ConsultaCreditoService.Application.Credito.ConsultarCreditoPorNumeroCredito;
using SharedKernel;

namespace ConsultaCreditoService.Api.Endpoints.Creditos;

internal sealed class ConsultarCreditoPorNumeroCredito : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/creditos/credito/{numeroCredito}", async (
            string numeroCredito,
            IQueryHandler<ConsultarCreditoPorNumeroCreditoQuery, ConsultarCreditoPorNumeroCreditoResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new ConsultarCreditoPorNumeroCreditoQuery(numeroCredito);
            Result<ConsultarCreditoPorNumeroCreditoResponse> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags("Creditos");
    }
}

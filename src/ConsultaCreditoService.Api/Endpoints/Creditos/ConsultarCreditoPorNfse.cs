
using ConsultaCreditoService.Api.Extensions;
using ConsultaCreditoService.Api.Infrastructure;
using ConsultaCreditoService.Application.Abstractions.Messaging;
using ConsultaCreditoService.Application.Credito.ConsultarCreditoPorNfse;
using SharedKernel;

namespace ConsultaCreditoService.Api.Endpoints.Creditos;

internal sealed class ConsultarCreditoPorNfse : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/creditos/{numeroNfse}", async (
            string numeroNfse,
            IQueryHandler<ConsultarCreditoPorNfseQuery, List<ConsultarCreditoPorNfseResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new ConsultarCreditoPorNfseQuery(numeroNfse);
            Result<List<ConsultarCreditoPorNfseResponse>> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags("Creditos");
    }
}

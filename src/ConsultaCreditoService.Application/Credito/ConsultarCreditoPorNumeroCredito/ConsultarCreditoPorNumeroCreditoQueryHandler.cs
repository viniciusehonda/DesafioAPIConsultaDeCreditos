using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultaCreditoService.Application.Abstractions.Messaging;
using ConsultaCreditoService.Application.Credito.ConsultarCreditoPorNfse;
using ConsultaCreditoService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace ConsultaCreditoService.Application.Credito.ConsultarCreditoPorNumeroCredito;
internal sealed class ConsultarCreditoPorNumeroCreditoQueryHandler(ConsultaCreditoServiceDbContext dbContext)
    : IQueryHandler<ConsultarCreditoPorNumeroCreditoQuery, ConsultarCreditoPorNumeroCreditoResponse>
{
    public async Task<Result<ConsultarCreditoPorNumeroCreditoResponse>> Handle(ConsultarCreditoPorNumeroCreditoQuery query, CancellationToken cancellationToken)
    {
        return await dbContext.Set<Domain.Entities.Credito>()
            .AsNoTracking()
            .Where(s => s.NumeroCredito == query.NumeroCredito)
            .Select(s => new ConsultarCreditoPorNumeroCreditoResponse(
                s.NumeroCredito,
                s.NumeroNfse,
                s.DataConstituicao.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                s.ValorIssqn,
                s.TipoCredito,
                s.IsSimplesNacional ? "Sim" : "Não",
                s.Aliquota,
                s.ValorFaturado,
                s.ValorDeducao,
                s.BaseCalculo
                ))
            .SingleOrDefaultAsync(cancellationToken);
    }
}

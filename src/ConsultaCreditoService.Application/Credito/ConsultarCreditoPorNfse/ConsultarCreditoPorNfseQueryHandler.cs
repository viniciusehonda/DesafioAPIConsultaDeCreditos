using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultaCreditoService.Application.Abstractions.Messaging;
using ConsultaCreditoService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace ConsultaCreditoService.Application.Credito.ConsultarCreditoPorNfse;
internal sealed class ConsultarCreditoPorNfseQueryHandler(ConsultaCreditoServiceDbContext dbContext)
    : IQueryHandler<ConsultarCreditoPorNfseQuery, List<ConsultarCreditoPorNfseResponse>>
{
    public async Task<Result<List<ConsultarCreditoPorNfseResponse>>> Handle(ConsultarCreditoPorNfseQuery query, CancellationToken cancellationToken)
    {
        return await dbContext.Set<Domain.Entities.Credito>()
            .AsNoTracking()
            .Where(s => s.NumeroNfse.Equals(query.NumeroNfse, StringComparison.Ordinal))
            .Select(s => new ConsultarCreditoPorNfseResponse(
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
            .ToListAsync(cancellationToken);
    }
}

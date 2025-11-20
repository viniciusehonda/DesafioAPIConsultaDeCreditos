using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultaCreditoService.Domain.Entities;
using ConsultaCreditoService.Domain.Repository;
using ConsultaCreditoService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ConsultaCreditoService.Infrastructure.Repositories;
public class CreditoRepository(ConsultaCreditoServiceDbContext consultaCreditoServiceDbContext) : ICreditoRepository
{
    public async Task AddCreditos(List<Credito> creditos, CancellationToken ct = default)
    {
        foreach(Credito credito in creditos)
        {
            await consultaCreditoServiceDbContext.AddAsync(credito, ct);
        }

        await consultaCreditoServiceDbContext.SaveChangesAsync(ct);
    }

    public async Task<List<Credito>> GetCreditosByNfse(string nfse, CancellationToken ct = default)
    {
        return await consultaCreditoServiceDbContext
            .Set<Credito>()
            .Where(s => s.NumeroNfse.Equals(nfse, StringComparison.Ordinal))
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<List<Credito>> GetCreditosByNumeroCredito(string numeroCredito, CancellationToken ct = default)
    {
        return await consultaCreditoServiceDbContext
            .Set<Credito>()
            .Where(s => s.NumeroCredito.Equals(numeroCredito, StringComparison.Ordinal))
            .AsNoTracking()
            .ToListAsync(ct);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultaCreditoService.Domain.Entities;

namespace ConsultaCreditoService.Domain.Repository;
public interface ICreditoRepository
{
    Task AddCredito(Credito credito, CancellationToken ct = default);
    Task<List<Credito>> GetCreditosByNfse(string nfse, CancellationToken ct = default);
    Task<List<Credito>> GetCreditosByNumeroCredito(string numeroCredito, CancellationToken ct = default); 
}

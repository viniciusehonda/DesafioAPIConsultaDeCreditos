using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultaCreditoService.Application.Abstractions.Messaging;

namespace ConsultaCreditoService.Application.Credito.ConsultarCreditoPorNfse;
public sealed record ConsultarCreditoPorNfseQuery(string NumeroNfse) : IQuery<List<ConsultarCreditoPorNfseResponse>>
{
}

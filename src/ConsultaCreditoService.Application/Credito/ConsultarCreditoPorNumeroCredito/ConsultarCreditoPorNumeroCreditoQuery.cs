using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultaCreditoService.Application.Abstractions.Messaging;

namespace ConsultaCreditoService.Application.Credito.ConsultarCreditoPorNumeroCredito;
public sealed record ConsultarCreditoPorNumeroCreditoQuery(string NumeroCredito) : IQuery<ConsultarCreditoPorNumeroCreditoResponse>
{
}

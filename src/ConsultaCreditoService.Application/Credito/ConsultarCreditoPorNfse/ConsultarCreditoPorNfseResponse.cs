using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaCreditoService.Application.Credito.ConsultarCreditoPorNfse;
public sealed record ConsultarCreditoPorNfseResponse(string NumeroCredito,
    string NumeroNfse,
    string DataConstituicao,
    decimal ValorIssqn,
    string TipoCredito,
    string SimplesNacional,
    decimal Aliquota,
    decimal ValorFaturado,
    decimal ValorDeducao,
    decimal BaseCalculo);

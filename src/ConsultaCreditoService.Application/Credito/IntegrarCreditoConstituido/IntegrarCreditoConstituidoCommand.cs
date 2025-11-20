using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ConsultaCreditoService.Application.Abstractions.Messaging;
using ICommand = ConsultaCreditoService.Application.Abstractions.Messaging.ICommand;

namespace ConsultaCreditoService.Application.Credito.IntegrarCreditoConstituido;
public sealed record IntegrarCreditoConstituidoCommand(List<IntegrarCreditoConstituidoDto> Items) : ICommand;

public sealed record IntegrarCreditoConstituidoDto(string NumeroCredito,
        string NumeroNfse,
        string DataConstituicao,
        decimal ValorIssqn,
        string TipoCredito,
        string SimplesNacional,
        decimal Aliquota,
        decimal ValorFaturado,
        decimal ValorDeducao,
        decimal BaseCalculo);

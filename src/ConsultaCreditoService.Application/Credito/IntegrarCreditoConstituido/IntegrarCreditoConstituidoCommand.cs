using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ConsultaCreditoService.Application.Abstractions.Messaging;
using ICommand = ConsultaCreditoService.Application.Abstractions.Messaging.ICommand;

namespace ConsultaCreditoService.Application.Credito.IntegrarCreditoConstituido;
public sealed record IntegrarCreditoConstituidoCommand(string NumeroCredito,
        string NumeroNfse,
        DateTime DataConstituicao,
        decimal ValorIssqn,
        string TipoCredito,
        bool SimplesNacional,
        decimal Aliquota,
        decimal ValorFaturado,
        decimal ValorDeducao,
        decimal BaseCalculo) : ICommand;

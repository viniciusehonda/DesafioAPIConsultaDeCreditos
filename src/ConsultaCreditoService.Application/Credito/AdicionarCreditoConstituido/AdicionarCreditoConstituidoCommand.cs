using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultaCreditoService.Application.Abstractions.Messaging;
using ConsultaCreditoService.Application.Credito.IntegrarCreditoConstituido;

namespace ConsultaCreditoService.Application.Credito.AdicionarCreditoConstituido;
public sealed record AdicionarCreditoConstituidoCommand(List<Domain.Entities.Credito> Items) : ICommand;

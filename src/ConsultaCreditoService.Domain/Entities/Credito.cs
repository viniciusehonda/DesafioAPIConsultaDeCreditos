using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaCreditoService.Domain.Entities;
public class Credito
{
    public long Id { get; }
    public string NumeroCredito { get; } = string.Empty;
    public string NumeroNfse { get; } = string.Empty;
    public DateTime DataConstituicao { get; } = DateTime.Now;
    public decimal ValorIssqn { get; } = decimal.Zero;
    public string TipoCredito { get; } = string.Empty;
    public bool IsSimplesNacional { get; }
    public decimal Aliquota { get; } = decimal.Zero;
    public decimal ValorFaturado { get; } = decimal.Zero;
    public decimal ValorDeducao { get; } = decimal.Zero;
    public decimal BaseCalculo { get; } = decimal.Zero;

    private Credito() { }

    public Credito(long id,
        string numeroCredito,
        string numeroNfse,
        DateTime dataConstituicao,
        decimal valorIssqn,
        string tipoCredito,
        bool isSimplesNacional,
        decimal aliquota,
        decimal valorFaturado,
        decimal valorDeducao,
        decimal baseCalculo)
    {
        Id = id;
        NumeroCredito = numeroCredito;
        NumeroNfse = numeroNfse;
        DataConstituicao = dataConstituicao;
        ValorIssqn = valorIssqn;
        TipoCredito = tipoCredito;
        IsSimplesNacional = isSimplesNacional;
        Aliquota = aliquota;
        ValorFaturado = valorFaturado;
        ValorDeducao = valorDeducao;
        BaseCalculo = baseCalculo;
    }

    public static Credito Create(long id,
        string numeroCredito,
        string numeroNfse,
        DateTime dataConstituicao,
        decimal valorIssqn,
        string tipoCredito,
        bool isSimplesNacional,
        decimal aliquota,
        decimal valorFaturado,
        decimal valorDeducao,
        decimal baseCalculo)
    {
        return new Credito(id,
            numeroCredito,
            numeroNfse,
            dataConstituicao,
            valorIssqn,
            tipoCredito,
            isSimplesNacional,
            aliquota,
            valorFaturado,
            valorDeducao,
            baseCalculo);
    }
}

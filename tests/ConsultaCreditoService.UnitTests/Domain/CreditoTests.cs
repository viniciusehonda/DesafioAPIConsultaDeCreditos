using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultaCreditoService.Domain.Entities;

namespace ConsultaCreditoService.UnitTests.Domain;

public class CreditoTests
{
    [Fact]
    public void Constructor_WithStringDate_ShouldParseDateCorrectly_AsUtc()
    {
        // Arrange
        string numeroCredito = "1234565";
        string numeroNfse = "NFSE999";
        string dataStr = "2024-02-25";
        decimal valorIssqn = 100m;
        string tipoCredito = "Normal";
        bool isSimples = true;
        decimal aliquota = 5m;
        decimal valorFaturado = 1000m;
        decimal valorDeducao = 50m;
        decimal baseCalculo = 950m;

        // Act
        var credito = new Credito(
            numeroCredito,
            numeroNfse,
            dataStr,
            valorIssqn,
            tipoCredito,
            isSimples,
            aliquota,
            valorFaturado,
            valorDeducao,
            baseCalculo
        );

        // Assert
        Assert.Equal(numeroCredito, credito.NumeroCredito);
        Assert.Equal(numeroNfse, credito.NumeroNfse);
        Assert.Equal(DateTimeKind.Utc, credito.DataConstituicao.Kind);
        Assert.Equal(DateTime.Parse("2024-02-25", CultureInfo.InvariantCulture), credito.DataConstituicao);
        Assert.Equal(valorIssqn, credito.ValorIssqn);
        Assert.Equal(tipoCredito, credito.TipoCredito);
        Assert.Equal(isSimples, credito.IsSimplesNacional);
        Assert.Equal(aliquota, credito.Aliquota);
        Assert.Equal(valorFaturado, credito.ValorFaturado);
        Assert.Equal(valorDeducao, credito.ValorDeducao);
        Assert.Equal(baseCalculo, credito.BaseCalculo);
    }

    [Theory]
    [InlineData("Sim", true)]
    [InlineData("sim", true)]
    [InlineData("SIM", true)]
    [InlineData("Não", false)]
    [InlineData("nao", false)]
    [InlineData("qualquer", false)]
    public void Create_ShouldInterpretSimplesNacionalCorrectly(string inputSimples, bool expected)
    {
        // Arrange
        string numeroCredito = "1414133";
        string numeroNfse = "N1";
        string dataStr = "2024-01-01";
        decimal valorIssqn = 10;
        string tipoCredito = "T1";
        decimal aliquota = 1;
        decimal valorFaturado = 100;
        decimal valorDeducao = 5;
        decimal baseCalculo = 95;

        // Act
        var credito = Credito.Create(
            numeroCredito,
            numeroNfse,
            dataStr,
            valorIssqn,
            tipoCredito,
            inputSimples,
            aliquota,
            valorFaturado,
            valorDeducao,
            baseCalculo
        );

        // Assert
        Assert.Equal(expected, credito.IsSimplesNacional);
    }

    [Fact]
    public void Create_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        string numeroCredito = "12313414";
        string numeroNfse = "NF999";
        string dataStr = "2023-12-31";
        decimal valorIssqn = 150;
        string tipoCredito = "Normal";
        string simples = "Sim";
        decimal aliquota = 4m;
        decimal valorFaturado = 2000m;
        decimal valorDeducao = 100m;
        decimal baseCalculo = 1900m;

        // Act
        var credito = Credito.Create(
            numeroCredito,
            numeroNfse,
            dataStr,
            valorIssqn,
            tipoCredito,
            simples,
            aliquota,
            valorFaturado,
            valorDeducao,
            baseCalculo
        );

        // Assert
        Assert.Equal(numeroCredito, credito.NumeroCredito);
        Assert.Equal(numeroNfse, credito.NumeroNfse);
        Assert.Equal(DateTimeKind.Utc, credito.DataConstituicao.Kind);
        Assert.Equal(DateTime.Parse("2023-12-31", CultureInfo.InvariantCulture), credito.DataConstituicao);
        Assert.Equal(valorIssqn, credito.ValorIssqn);
        Assert.Equal(tipoCredito, credito.TipoCredito);
        Assert.True(credito.IsSimplesNacional);
        Assert.Equal(aliquota, credito.Aliquota);
        Assert.Equal(valorFaturado, credito.ValorFaturado);
        Assert.Equal(valorDeducao, credito.ValorDeducao);
        Assert.Equal(baseCalculo, credito.BaseCalculo);
    }
}

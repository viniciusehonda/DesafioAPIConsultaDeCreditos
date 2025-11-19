using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConsultaCreditoService.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class CreateCredito : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "public");

        migrationBuilder.CreateTable(
            name: "credito",
            schema: "public",
            columns: table => new
            {
                id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                numero_credito = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                numero_nfse = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                data_constituicao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                valor_issqn = table.Column<decimal>(type: "numeric(15,2)", precision: 15, scale: 2, nullable: false),
                tipo_credito = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                simples_nacional = table.Column<bool>(type: "boolean", nullable: false),
                aliquota = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                valor_faturado = table.Column<decimal>(type: "numeric(15,2)", precision: 15, scale: 2, nullable: false),
                valor_deducao = table.Column<decimal>(type: "numeric(15,2)", precision: 15, scale: 2, nullable: false),
                base_calculo = table.Column<decimal>(type: "numeric(15,2)", precision: 15, scale: 2, nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_credito", x => x.id));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "credito",
            schema: "public");
    }
}

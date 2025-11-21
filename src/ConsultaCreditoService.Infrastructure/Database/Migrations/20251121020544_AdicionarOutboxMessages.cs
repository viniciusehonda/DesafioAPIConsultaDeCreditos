using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsultaCreditoService.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class AdicionarOutboxMessages : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "outbox",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                topic = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                payload = table.Column<string>(type: "character varying(350)", maxLength: 350, nullable: false),
                processed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_outbox", x => x.id));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "outbox",
            schema: "public");
    }
}

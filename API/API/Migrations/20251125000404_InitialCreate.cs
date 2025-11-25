using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chamados",
                columns: table => new
                {
                    ChamadoId = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chamados", x => x.ChamadoId);
                });

            migrationBuilder.InsertData(
                table: "Chamados",
                columns: new[] { "ChamadoId", "CriadoEm", "Descricao", "Status" },
                values: new object[] { "f4a2b8d9-7c4e-4f8e-bdc9-9181e456ad0e", new DateTime(2025, 11, 24, 21, 4, 3, 488, DateTimeKind.Local).AddTicks(7643), "TESTE", "Aberto" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chamados");
        }
    }
}

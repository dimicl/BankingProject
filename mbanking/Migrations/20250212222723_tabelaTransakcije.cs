using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTemplate.Migrations
{
    /// <inheritdoc />
    public partial class tabelaTransakcije : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transakcije",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    iznos = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RacunId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transakcije", x => x.id);
                    table.ForeignKey(
                        name: "FK_Transakcije_Racuni_RacunId",
                        column: x => x.RacunId,
                        principalTable: "Racuni",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transakcije_RacunId",
                table: "Transakcije",
                column: "RacunId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transakcije");
        }
    }
}

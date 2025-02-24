using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTemplate.Migrations
{
    /// <inheritdoc />
    public partial class stednja : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stednje",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cilj = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Korisnikid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stednje", x => x.id);
                    table.ForeignKey(
                        name: "FK_Stednje_Korisnici_Korisnikid",
                        column: x => x.Korisnikid,
                        principalTable: "Korisnici",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stednje_Korisnikid",
                table: "Stednje",
                column: "Korisnikid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stednje");
        }
    }
}

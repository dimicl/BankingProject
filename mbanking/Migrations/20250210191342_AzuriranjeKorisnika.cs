using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTemplate.Migrations
{
    /// <inheritdoc />
    public partial class AzuriranjeKorisnika : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Racuni",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    brojRacuna = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sredstva = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    valuta = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racuni", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    jmbg = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    brojTelefona = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RacunId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.id);
                    table.ForeignKey(
                        name: "FK_Korisnici_Racuni_RacunId",
                        column: x => x.RacunId,
                        principalTable: "Racuni",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_RacunId",
                table: "Korisnici",
                column: "RacunId",
                unique: true,
                filter: "[RacunId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "Racuni");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTemplate.Migrations
{
    /// <inheritdoc />
    public partial class novaIzmena : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "adresa",
                table: "Korisnici");

            migrationBuilder.DropColumn(
                name: "brojTelefona",
                table: "Korisnici");

            migrationBuilder.DropColumn(
                name: "jmbg",
                table: "Korisnici");

            migrationBuilder.AddColumn<string>(
                name: "pin",
                table: "Korisnici",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pin",
                table: "Korisnici");

            migrationBuilder.AddColumn<string>(
                name: "adresa",
                table: "Korisnici",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "brojTelefona",
                table: "Korisnici",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "jmbg",
                table: "Korisnici",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTemplate.Migrations
{
    /// <inheritdoc />
    public partial class izmenaTransakcije : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transakcije_Racuni_RacunId",
                table: "Transakcije");

            migrationBuilder.RenameColumn(
                name: "RacunId",
                table: "Transakcije",
                newName: "Racunid");

            migrationBuilder.RenameIndex(
                name: "IX_Transakcije_RacunId",
                table: "Transakcije",
                newName: "IX_Transakcije_Racunid");

            migrationBuilder.AlterColumn<int>(
                name: "Racunid",
                table: "Transakcije",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "TekuciReceiver",
                table: "Transakcije",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TekuciSender",
                table: "Transakcije",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Transakcije_Racuni_Racunid",
                table: "Transakcije",
                column: "Racunid",
                principalTable: "Racuni",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transakcije_Racuni_Racunid",
                table: "Transakcije");

            migrationBuilder.DropColumn(
                name: "TekuciReceiver",
                table: "Transakcije");

            migrationBuilder.DropColumn(
                name: "TekuciSender",
                table: "Transakcije");

            migrationBuilder.RenameColumn(
                name: "Racunid",
                table: "Transakcije",
                newName: "RacunId");

            migrationBuilder.RenameIndex(
                name: "IX_Transakcije_Racunid",
                table: "Transakcije",
                newName: "IX_Transakcije_RacunId");

            migrationBuilder.AlterColumn<int>(
                name: "RacunId",
                table: "Transakcije",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transakcije_Racuni_RacunId",
                table: "Transakcije",
                column: "RacunId",
                principalTable: "Racuni",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

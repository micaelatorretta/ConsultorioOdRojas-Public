using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class historiaclinicahabitos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Bebe_Cantidad",
                table: "HistoriaClinica",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Bebe_Tiene",
                table: "HistoriaClinica",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Fuma_Cantidad",
                table: "HistoriaClinica",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Fuma_Tiene",
                table: "HistoriaClinica",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bebe_Cantidad",
                table: "HistoriaClinica");

            migrationBuilder.DropColumn(
                name: "Bebe_Tiene",
                table: "HistoriaClinica");

            migrationBuilder.DropColumn(
                name: "Fuma_Cantidad",
                table: "HistoriaClinica");

            migrationBuilder.DropColumn(
                name: "Fuma_Tiene",
                table: "HistoriaClinica");
        }
    }
}

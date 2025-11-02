using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class utilidadesodontograma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ColorHexadecimal",
                table: "OdontogramaCaraDental",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColorHexSugerido",
                table: "Nomenclador",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PermiteMultiple",
                table: "Nomenclador",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequiereCara",
                table: "Nomenclador",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorHexadecimal",
                table: "OdontogramaCaraDental");

            migrationBuilder.DropColumn(
                name: "ColorHexSugerido",
                table: "Nomenclador");

            migrationBuilder.DropColumn(
                name: "PermiteMultiple",
                table: "Nomenclador");

            migrationBuilder.DropColumn(
                name: "RequiereCara",
                table: "Nomenclador");
        }
    }
}

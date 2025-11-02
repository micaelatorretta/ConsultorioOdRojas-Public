using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ContactoTelefonicoVO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Contacto_Extension",
                table: "EntidadDummy",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contacto_NumeroTelefono",
                table: "EntidadDummy",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Contacto_Tipo",
                table: "EntidadDummy",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contacto_Extension",
                table: "EntidadDummy");

            migrationBuilder.DropColumn(
                name: "Contacto_NumeroTelefono",
                table: "EntidadDummy");

            migrationBuilder.DropColumn(
                name: "Contacto_Tipo",
                table: "EntidadDummy");
        }
    }
}

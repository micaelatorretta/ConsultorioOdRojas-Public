using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class borradologico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Usuario",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Turno",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ObraSocial",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "EntidadDummyD",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "EntidadDummyC",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "EntidadDummyB",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "EntidadDummy",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Turno");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ObraSocial");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "EntidadDummyD");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "EntidadDummyC");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "EntidadDummyB");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "EntidadDummy");
        }
    }
}

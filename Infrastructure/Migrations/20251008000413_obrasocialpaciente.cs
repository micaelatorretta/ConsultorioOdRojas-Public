using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class obrasocialpaciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ObraSocialId",
                table: "Paciente",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_ObraSocialId",
                table: "Paciente",
                column: "ObraSocialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Paciente_ObraSocial_ObraSocialId",
                table: "Paciente",
                column: "ObraSocialId",
                principalTable: "ObraSocial",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paciente_ObraSocial_ObraSocialId",
                table: "Paciente");

            migrationBuilder.DropIndex(
                name: "IX_Paciente_ObraSocialId",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "ObraSocialId",
                table: "Paciente");
        }
    }
}

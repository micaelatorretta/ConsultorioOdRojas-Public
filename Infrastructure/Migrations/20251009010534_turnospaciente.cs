using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class turnospaciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PacienteId",
                table: "Turno",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Turno_PacienteId",
                table: "Turno",
                column: "PacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Turno_Paciente_PacienteId",
                table: "Turno",
                column: "PacienteId",
                principalTable: "Paciente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turno_Paciente_PacienteId",
                table: "Turno");

            migrationBuilder.DropIndex(
                name: "IX_Turno_PacienteId",
                table: "Turno");

            migrationBuilder.DropColumn(
                name: "PacienteId",
                table: "Turno");
        }
    }
}

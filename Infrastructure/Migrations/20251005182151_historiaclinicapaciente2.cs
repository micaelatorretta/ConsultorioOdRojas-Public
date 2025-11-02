using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class historiaclinicapaciente2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paciente_HistoriaClinica_HistoriaClinicaId",
                table: "Paciente");

            migrationBuilder.DropIndex(
                name: "IX_Paciente_HistoriaClinicaId",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "HistoriaClinicaId",
                table: "Paciente");

            migrationBuilder.AddColumn<int>(
                name: "PacienteId",
                table: "HistoriaClinica",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HistoriaClinica_PacienteId",
                table: "HistoriaClinica",
                column: "PacienteId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoriaClinica_Paciente_PacienteId",
                table: "HistoriaClinica",
                column: "PacienteId",
                principalTable: "Paciente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoriaClinica_Paciente_PacienteId",
                table: "HistoriaClinica");

            migrationBuilder.DropIndex(
                name: "IX_HistoriaClinica_PacienteId",
                table: "HistoriaClinica");

            migrationBuilder.DropColumn(
                name: "PacienteId",
                table: "HistoriaClinica");

            migrationBuilder.AddColumn<int>(
                name: "HistoriaClinicaId",
                table: "Paciente",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_HistoriaClinicaId",
                table: "Paciente",
                column: "HistoriaClinicaId",
                unique: true,
                filter: "[HistoriaClinicaId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Paciente_HistoriaClinica_HistoriaClinicaId",
                table: "Paciente",
                column: "HistoriaClinicaId",
                principalTable: "HistoriaClinica",
                principalColumn: "Id");
        }
    }
}

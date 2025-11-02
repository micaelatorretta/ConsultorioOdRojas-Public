using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class historiaclinicapaciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}

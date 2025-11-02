using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class odontogramapaciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PacienteId",
                table: "Odontograma",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Vigente",
                table: "Odontograma",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Odontograma_PacienteId",
                table: "Odontograma",
                column: "PacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Odontograma_Paciente_PacienteId",
                table: "Odontograma",
                column: "PacienteId",
                principalTable: "Paciente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Odontograma_Paciente_PacienteId",
                table: "Odontograma");

            migrationBuilder.DropIndex(
                name: "IX_Odontograma_PacienteId",
                table: "Odontograma");

            migrationBuilder.DropColumn(
                name: "PacienteId",
                table: "Odontograma");

            migrationBuilder.DropColumn(
                name: "Vigente",
                table: "Odontograma");
        }
    }
}

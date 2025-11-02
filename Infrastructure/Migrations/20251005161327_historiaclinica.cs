using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class historiaclinica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoriaClinica",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IntervencionesUlt2Anios_Si = table.Column<bool>(type: "bit", nullable: false),
                    IntervencionesUlt2Anios_Detalle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TratamientoMedico_Si = table.Column<bool>(type: "bit", nullable: false),
                    TratamientoMedico_Detalle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Alergias_Si = table.Column<bool>(type: "bit", nullable: false),
                    Alergias_Detalle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    AnestesiasPrevias_Si = table.Column<bool>(type: "bit", nullable: false),
                    AnestesiasPrevias_Detalle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SangradoExcesivo_Si = table.Column<bool>(type: "bit", nullable: false),
                    SangradoExcesivo_Detalle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CansancioAlCaminar = table.Column<bool>(type: "bit", nullable: false),
                    Embarazo = table.Column<bool>(type: "bit", nullable: false),
                    MedicacionActual_Si = table.Column<bool>(type: "bit", nullable: false),
                    MedicacionActual_Detalle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Radiografias_Si = table.Column<bool>(type: "bit", nullable: false),
                    Radiografias_Fecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoriaClinica", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoriaClinica");
        }
    }
}

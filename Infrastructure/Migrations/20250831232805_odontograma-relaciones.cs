using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class odontogramarelaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OdontogramaPiezaDental",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PiezaDentalId = table.Column<int>(type: "int", nullable: false),
                    OdontogramaId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OdontogramaPiezaDental", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OdontogramaPiezaDental_Odontograma_OdontogramaId",
                        column: x => x.OdontogramaId,
                        principalTable: "Odontograma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OdontogramaPiezaDental_PiezaDental_PiezaDentalId",
                        column: x => x.PiezaDentalId,
                        principalTable: "PiezaDental",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OdontogramaCaraDental",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaraDentalId = table.Column<int>(type: "int", nullable: false),
                    PiezaDentalId = table.Column<int>(type: "int", nullable: false),
                    NomencladorId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OdontogramaCaraDental", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OdontogramaCaraDental_CaraDental_CaraDentalId",
                        column: x => x.CaraDentalId,
                        principalTable: "CaraDental",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OdontogramaCaraDental_Nomenclador_NomencladorId",
                        column: x => x.NomencladorId,
                        principalTable: "Nomenclador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OdontogramaCaraDental_OdontogramaPiezaDental_PiezaDentalId",
                        column: x => x.PiezaDentalId,
                        principalTable: "OdontogramaPiezaDental",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OdontogramaCaraDental_CaraDentalId",
                table: "OdontogramaCaraDental",
                column: "CaraDentalId");

            migrationBuilder.CreateIndex(
                name: "IX_OdontogramaCaraDental_NomencladorId",
                table: "OdontogramaCaraDental",
                column: "NomencladorId");

            migrationBuilder.CreateIndex(
                name: "IX_OdontogramaCaraDental_PiezaDentalId",
                table: "OdontogramaCaraDental",
                column: "PiezaDentalId");

            migrationBuilder.CreateIndex(
                name: "IX_OdontogramaPiezaDental_OdontogramaId",
                table: "OdontogramaPiezaDental",
                column: "OdontogramaId");

            migrationBuilder.CreateIndex(
                name: "IX_OdontogramaPiezaDental_PiezaDentalId",
                table: "OdontogramaPiezaDental",
                column: "PiezaDentalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OdontogramaCaraDental");

            migrationBuilder.DropTable(
                name: "OdontogramaPiezaDental");
        }
    }
}

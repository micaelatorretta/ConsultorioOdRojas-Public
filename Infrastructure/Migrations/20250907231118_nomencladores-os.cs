using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class nomencladoresos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaPrestacion",
                table: "OdontogramaCaraDental",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ObraSocialId",
                table: "OdontogramaCaraDental",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ObraSocialId",
                table: "Nomenclador",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OdontogramaCaraDental_ObraSocialId",
                table: "OdontogramaCaraDental",
                column: "ObraSocialId");

            migrationBuilder.CreateIndex(
                name: "IX_Nomenclador_ObraSocialId",
                table: "Nomenclador",
                column: "ObraSocialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nomenclador_ObraSocial_ObraSocialId",
                table: "Nomenclador",
                column: "ObraSocialId",
                principalTable: "ObraSocial",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OdontogramaCaraDental_ObraSocial_ObraSocialId",
                table: "OdontogramaCaraDental",
                column: "ObraSocialId",
                principalTable: "ObraSocial",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nomenclador_ObraSocial_ObraSocialId",
                table: "Nomenclador");

            migrationBuilder.DropForeignKey(
                name: "FK_OdontogramaCaraDental_ObraSocial_ObraSocialId",
                table: "OdontogramaCaraDental");

            migrationBuilder.DropIndex(
                name: "IX_OdontogramaCaraDental_ObraSocialId",
                table: "OdontogramaCaraDental");

            migrationBuilder.DropIndex(
                name: "IX_Nomenclador_ObraSocialId",
                table: "Nomenclador");

            migrationBuilder.DropColumn(
                name: "FechaPrestacion",
                table: "OdontogramaCaraDental");

            migrationBuilder.DropColumn(
                name: "ObraSocialId",
                table: "OdontogramaCaraDental");

            migrationBuilder.DropColumn(
                name: "ObraSocialId",
                table: "Nomenclador");
        }
    }
}

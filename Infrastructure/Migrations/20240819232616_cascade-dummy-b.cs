using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class cascadedummyb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntidadDummyB_EntidadDummy_EntidadDummyId",
                table: "EntidadDummyB");

            migrationBuilder.AddForeignKey(
                name: "FK_EntidadDummyB_EntidadDummy_EntidadDummyId",
                table: "EntidadDummyB",
                column: "EntidadDummyId",
                principalTable: "EntidadDummy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntidadDummyB_EntidadDummy_EntidadDummyId",
                table: "EntidadDummyB");

            migrationBuilder.AddForeignKey(
                name: "FK_EntidadDummyB_EntidadDummy_EntidadDummyId",
                table: "EntidadDummyB",
                column: "EntidadDummyId",
                principalTable: "EntidadDummy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

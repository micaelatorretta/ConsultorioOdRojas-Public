using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fkentidaddummyc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntidadDummyC_EntidadDummy_EntidadDummy.Id",
                table: "EntidadDummyC");

            migrationBuilder.RenameColumn(
                name: "EntidadDummy.Id",
                table: "EntidadDummyC",
                newName: "EntidadDummyId");

            migrationBuilder.RenameIndex(
                name: "IX_EntidadDummyC_EntidadDummy.Id",
                table: "EntidadDummyC",
                newName: "IX_EntidadDummyC_EntidadDummyId");

            migrationBuilder.AddForeignKey(
                name: "FK_EntidadDummyC_EntidadDummy_EntidadDummyId",
                table: "EntidadDummyC",
                column: "EntidadDummyId",
                principalTable: "EntidadDummy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntidadDummyC_EntidadDummy_EntidadDummyId",
                table: "EntidadDummyC");

            migrationBuilder.RenameColumn(
                name: "EntidadDummyId",
                table: "EntidadDummyC",
                newName: "EntidadDummy.Id");

            migrationBuilder.RenameIndex(
                name: "IX_EntidadDummyC_EntidadDummyId",
                table: "EntidadDummyC",
                newName: "IX_EntidadDummyC_EntidadDummy.Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EntidadDummyC_EntidadDummy_EntidadDummy.Id",
                table: "EntidadDummyC",
                column: "EntidadDummy.Id",
                principalTable: "EntidadDummy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

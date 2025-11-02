using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class relacionpiezacaradental : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PiezaDentalId",
                table: "CaraDental",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CaraDental_PiezaDentalId",
                table: "CaraDental",
                column: "PiezaDentalId");

            migrationBuilder.AddForeignKey(
                name: "FK_CaraDental_PiezaDental_PiezaDentalId",
                table: "CaraDental",
                column: "PiezaDentalId",
                principalTable: "PiezaDental",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaraDental_PiezaDental_PiezaDentalId",
                table: "CaraDental");

            migrationBuilder.DropIndex(
                name: "IX_CaraDental_PiezaDentalId",
                table: "CaraDental");

            migrationBuilder.DropColumn(
                name: "PiezaDentalId",
                table: "CaraDental");
        }
    }
}

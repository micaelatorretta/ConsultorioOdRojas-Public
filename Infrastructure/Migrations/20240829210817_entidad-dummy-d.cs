using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class entidaddummyd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntidadDummyD",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntidadDummyD", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntidadDummyDEntidadDummy",
                columns: table => new
                {
                    DummiesAId = table.Column<int>(type: "int", nullable: false),
                    DummiesDId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntidadDummyDEntidadDummy", x => new { x.DummiesAId, x.DummiesDId });
                    table.ForeignKey(
                        name: "FK_EntidadDummyDEntidadDummy_EntidadDummyD_DummiesDId",
                        column: x => x.DummiesDId,
                        principalTable: "EntidadDummyD",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntidadDummyDEntidadDummy_EntidadDummy_DummiesAId",
                        column: x => x.DummiesAId,
                        principalTable: "EntidadDummy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntidadDummyDEntidadDummy_DummiesDId",
                table: "EntidadDummyDEntidadDummy",
                column: "DummiesDId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntidadDummyDEntidadDummy");

            migrationBuilder.DropTable(
                name: "EntidadDummyD");
        }
    }
}

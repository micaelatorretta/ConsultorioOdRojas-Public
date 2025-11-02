using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntidadDummy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntidadDummy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntidadDummyB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EntidadDummyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntidadDummyB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntidadDummyB_EntidadDummy_EntidadDummyId",
                        column: x => x.EntidadDummyId,
                        principalTable: "EntidadDummy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntidadDummyC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EntidadDummyId = table.Column<int>(name: "EntidadDummy.Id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntidadDummyC", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntidadDummyC_EntidadDummy_EntidadDummy.Id",
                        column: x => x.EntidadDummyId,
                        principalTable: "EntidadDummy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntidadDummyB_EntidadDummyId",
                table: "EntidadDummyB",
                column: "EntidadDummyId");

            migrationBuilder.CreateIndex(
                name: "IX_EntidadDummyC_EntidadDummy.Id",
                table: "EntidadDummyC",
                column: "EntidadDummy.Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntidadDummyB");

            migrationBuilder.DropTable(
                name: "EntidadDummyC");

            migrationBuilder.DropTable(
                name: "EntidadDummy");
        }
    }
}

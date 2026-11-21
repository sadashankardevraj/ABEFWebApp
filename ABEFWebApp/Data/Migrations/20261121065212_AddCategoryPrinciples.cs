using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ABEFWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryPrinciples : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryPrinciples",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    PrincipleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryPrinciples", x => new { x.CategoryId, x.PrincipleId });
                    table.ForeignKey(
                        name: "FK_CategoryPrinciples_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryPrinciples_Principles_PrincipleId",
                        column: x => x.PrincipleId,
                        principalTable: "Principles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CategoryPrinciples",
                columns: new[] { "CategoryId", "PrincipleId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 8 },
                    { 1, 9 },
                    { 2, 3 },
                    { 2, 7 },
                    { 3, 1 },
                    { 3, 4 },
                    { 3, 7 },
                    { 4, 5 },
                    { 4, 6 },
                    { 5, 6 },
                    { 5, 7 },
                    { 6, 4 },
                    { 6, 6 },
                    { 6, 7 },
                    { 7, 3 },
                    { 7, 7 },
                    { 7, 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryPrinciples_PrincipleId",
                table: "CategoryPrinciples",
                column: "PrincipleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryPrinciples");
        }
    }
}

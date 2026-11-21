using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ABEFWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RegisterUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Date", "IsPublished", "Location", "RegisterUrl", "ShortDescription", "Title", "Type" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 12, 4, 10, 0, 0, 0, DateTimeKind.Unspecified), true, "Online", "#", "Sample event: a short online introduction to the Australian Business Excellence Framework.", "ABEF Introduction Webinar", "Webinar" },
                    { 2, new DateTime(2027, 1, 15, 9, 30, 0, 0, DateTimeKind.Unspecified), true, "Sydney", "#", "Sample event: a hands-on workshop on using ADRI for reflection and improvement.", "ADRI Workshop", "Workshop" },
                    { 3, new DateTime(2027, 2, 12, 13, 0, 0, 0, DateTimeKind.Unspecified), true, "Melbourne", "#", "Sample event: learn how to run a practical organisational self-assessment.", "Self-Assessment Masterclass", "Workshop" },
                    { 4, new DateTime(2027, 3, 20, 9, 0, 0, 0, DateTimeKind.Unspecified), true, "Brisbane", "#", "Sample event: a full-day forum sharing practice, results and lessons from excellence work.", "Business Excellence Forum", "Conference" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}

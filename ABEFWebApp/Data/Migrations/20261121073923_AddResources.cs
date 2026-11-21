using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ABEFWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddResources : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Section = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ResourceType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(260)", maxLength: 260, nullable: true),
                    ExternalUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resources_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "ExternalUrl", "FileName", "IsPublished", "ResourceType", "Section", "Summary", "Title" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2026, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, true, "PDF", "YourCopyOfAbef", "Provided by BEA to members. File to be added by Admin.", "ABEF 2026 Framework (member copy)" },
                    { 2, 1, new DateTime(2026, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "sample-guide.pdf", true, "PDF", "HowToApply", "Sample content: a short guide to using Approach, Deployment, Results and Improvement on one item.", "Sample: Getting started with ADRI" },
                    { 3, 1, new DateTime(2026, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "sample-guide.pdf", true, "PDF", "HowToApply", "Sample content: practical tips for exploring Category 1 with your team.", "Sample: Leadership category walkthrough" },
                    { 4, 7, new DateTime(2026, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://example.com/sample-bpir", null, true, "Link", "Bpir", "Sample content: how benchmarking resources can support improvement planning.", "Sample: BPIR overview for members" },
                    { 5, 2, new DateTime(2026, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://example.com/sample-webinar-customer-value", null, true, "Video", "Webinars", "Sample content: a short webinar on understanding customer and stakeholder value.", "Sample: Customer value webinar recording" },
                    { 6, 3, new DateTime(2026, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://example.com/sample-webinar-strategy", null, true, "Video", "Webinars", "Sample content: turning direction into practical plans and goals.", "Sample: Strategy and planning webinar" },
                    { 7, 4, new DateTime(2026, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "sample-guide.pdf", true, "PDF", "Documents", "Sample content: a printable checklist for reviewing workforce capability.", "Sample: People capability checklist" },
                    { 8, null, new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "sample-guide.pdf", true, "PDF", "Education", "Sample content: teaching notes for introducing the framework to learners.", "Sample: Education pack – ABEF basics" },
                    { 9, 6, new DateTime(2026, 11, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "sample-case-study.pdf", true, "PDF", "CaseStudies", "Sample content: how one organisation improved a key process using ADRI.", "Sample: Case study – process improvement" },
                    { 10, 5, new DateTime(2026, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "sample-case-study.pdf", true, "PDF", "CaseStudies", "Sample content: using better data and knowledge to guide decisions.", "Sample: Case study – information for decisions" },
                    { 11, 7, new DateTime(2026, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "sample-template.pdf", true, "Template", "Tools", "Sample content: a simple template to plan a first organisational self-assessment.", "Sample: Self-assessment planning template" },
                    { 12, 7, new DateTime(2026, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "sample-template.pdf", true, "Template", "Tools", "Sample content: a worksheet for reviewing results and sustainable performance.", "Sample: Results review worksheet" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resources_CategoryId",
                table: "Resources",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_Section",
                table: "Resources",
                column: "Section");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resources");
        }
    }
}

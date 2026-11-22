using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ABEFWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAssessments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssessmentQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    QuestionText = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssessmentQuestions_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Assessments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    OrganisationName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CurrentCategory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assessments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssessmentId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssessmentAnswers_AssessmentQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "AssessmentQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssessmentAnswers_Assessments_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AssessmentQuestions",
                columns: new[] { "Id", "ItemId", "QuestionText", "SortOrder" },
                values: new object[,]
                {
                    { 1, 1, "How well do leaders set clear expectations and govern the whole organisation?", 1 },
                    { 2, 2, "How well do leaders shape a culture that supports excellence every day?", 2 },
                    { 3, 3, "How well does a clear purpose and vision guide the organisation?", 3 },
                    { 4, 4, "How well does the organisation act responsibly toward society, community and the environment?", 4 },
                    { 5, 5, "How well does the organisation understand its markets, customers and other stakeholders?", 5 },
                    { 6, 6, "How well are relationships with customers and other stakeholders managed?", 6 },
                    { 7, 7, "How well does the organisation measure how customers and stakeholders judge the value they receive?", 7 },
                    { 8, 8, "How well do leaders understand the wider environment and the organisation’s capability?", 8 },
                    { 9, 9, "How well are plans shaped by what stakeholders need and expect?", 9 },
                    { 10, 10, "How well does planning turn direction into practical goals and actions?", 10 },
                    { 11, 11, "How well is people capability built to meet the organisation’s goals?", 11 },
                    { 12, 12, "How well are people supported to grow and perform effectively?", 12 },
                    { 13, 13, "How well does the workplace support wellbeing, engagement and pride in work?", 13 },
                    { 14, 14, "How well is the right data gathered and analysed to guide decisions?", 14 },
                    { 15, 15, "How well is knowledge used in ways that create real value?", 15 },
                    { 16, 16, "How well are technology and new ideas used to improve how work is done?", 16 },
                    { 17, 17, "How well are key organisational processes identified, owned and managed?", 17 },
                    { 18, 18, "How well are processes with suppliers and stakeholders managed?", 18 },
                    { 19, 19, "How well are processes improved and innovated over time?", 19 },
                    { 20, 20, "How well are organisational results measured and communicated?", 20 },
                    { 21, 21, "How well is performance sustained for the long term, not only short-term gains?", 21 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentAnswers_AssessmentId_QuestionId",
                table: "AssessmentAnswers",
                columns: new[] { "AssessmentId", "QuestionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentAnswers_QuestionId",
                table: "AssessmentAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentQuestions_ItemId",
                table: "AssessmentQuestions",
                column: "ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentQuestions_SortOrder",
                table: "AssessmentQuestions",
                column: "SortOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_Status",
                table: "Assessments",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_UserId",
                table: "Assessments",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssessmentAnswers");

            migrationBuilder.DropTable(
                name: "AssessmentQuestions");

            migrationBuilder.DropTable(
                name: "Assessments");
        }
    }
}

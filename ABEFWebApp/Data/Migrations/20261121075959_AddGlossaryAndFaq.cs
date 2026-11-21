using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ABEFWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGlossaryAndFaq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Faqs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faqs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GlossaryTerms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Term = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Definition = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlossaryTerms", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Faqs",
                columns: new[] { "Id", "Answer", "Question", "SortOrder" },
                values: new object[,]
                {
                    { 1, "The Australian Business Excellence Framework is a practical guide for leading, improving and sustaining organisational performance. It uses seven categories, principles and ADRI to help teams focus on what matters.", "What is the ABEF?", 1 },
                    { 2, "Individuals, students, educators and organisations can join BEA. Membership plans cover students, practitioners, foundations, businesses and larger corporations.", "Who can become a member?", 2 },
                    { 3, "Log in as a member, open Assessment & Scoring from your dashboard, and begin with one category or item. Use ADRI to reflect on current practice before you capture evidence and scores.", "How do I start a self-assessment?", 3 },
                    { 4, "ADRI stands for Approach, Deployment, Results and Improvement. It is a simple cycle for planning work, putting it into action, checking outcomes and learning what to change next.", "What is ADRI?", 4 },
                    { 5, "Scoring tools will help members judge practice and evidence against the framework. Full scoring features are being expanded in later releases of the Digital ABEF.", "How are results scored?", 5 },
                    { 6, "Members can access framework materials in the Knowledge Hub under Your Copy of ABEF. Some files are added by BEA administrators as they become available.", "Can I download the ABEF?", 6 },
                    { 7, "On the Member Login page, choose Forgot password and follow the email instructions. After you reset it, sign in again with your new password.", "How do I reset my password?", 7 },
                    { 8, "Use the Contact Us page on this site to send a message. You can also reach BEA through the contact details listed in membership and event communications.", "How do I contact BEA?", 8 }
                });

            migrationBuilder.InsertData(
                table: "GlossaryTerms",
                columns: new[] { "Id", "Definition", "Term" },
                values: new object[,]
                {
                    { 1, "A simple cycle used to plan, put into action, check results and improve any part of the organisation.", "ADRI" },
                    { 2, "The thinking and planning behind how work will be done — the intended way of working.", "Approach" },
                    { 3, "Putting the planned approach into action across teams, sites and day-to-day work.", "Deployment" },
                    { 4, "The evidence that shows whether the approach and deployment are delivering the intended outcomes.", "Results" },
                    { 5, "Learning from results and adapting practice so performance gets better over time.", "Improvement" },
                    { 6, "One of the seven main areas of the ABEF that groups related items for assessment and improvement.", "Category" },
                    { 7, "A focused topic inside a category that organisations reflect on using ADRI.", "Item" },
                    { 8, "A category type that sets direction for the organisation, such as leadership or customers.", "Driver" },
                    { 9, "A category type that supports how work gets done, such as people, processes or knowledge.", "Enabler" },
                    { 10, "A guiding idea behind the framework that shapes excellent organisational practice.", "Principle" },
                    { 11, "A disciplined way of leading and improving so an organisation creates lasting value.", "Business Excellence" },
                    { 12, "A structured review of organisational practice against the framework to find strengths and gaps.", "Self-Assessment" },
                    { 13, "Anyone who has an interest in the organisation’s work, such as customers, staff, partners or the community.", "Stakeholder" },
                    { 14, "Comparing practices and results with others to learn what good looks like and where to improve.", "Benchmarking" },
                    { 15, "An ongoing habit of reviewing work, learning from evidence and making steady, practical changes.", "Continuous Improvement" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Faqs_SortOrder",
                table: "Faqs",
                column: "SortOrder");

            migrationBuilder.CreateIndex(
                name: "IX_GlossaryTerms_Term",
                table: "GlossaryTerms",
                column: "Term",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Faqs");

            migrationBuilder.DropTable(
                name: "GlossaryTerms");
        }
    }
}

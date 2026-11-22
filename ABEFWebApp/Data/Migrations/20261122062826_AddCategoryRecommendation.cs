using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABEFWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryRecommendation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Recommendation",
                table: "Categories",
                type: "nvarchar(800)",
                maxLength: 800,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Recommendation",
                value: "Revisit how leaders set expectations and oversee decisions. Ask teams where governance feels unclear, then use ADRI to strengthen one leadership or culture practice.");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Recommendation",
                value: "Map your main customers and stakeholders and check that value is defined in their terms. Gather fresh feedback, then use ADRI to improve one relationship or service experience.");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Recommendation",
                value: "Test whether plans reflect real pressures and stakeholder needs. Confirm goals cascade into team actions, then use ADRI to tighten one planning cycle.");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "Recommendation",
                value: "Review how people are developed and supported. Ask staff what helps them do their best work and use ADRI to plan one improvement.");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "Recommendation",
                value: "Confirm leaders share the same core measures and that useful knowledge is easy to find. Remove noise from the data that matters most, then use ADRI to improve one information practice.");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "Recommendation",
                value: "Name owners for the processes that matter most and map where handoffs slow work down. Protect time for both small fixes and bigger ideas, then use ADRI on one process.");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                column: "Recommendation",
                value: "Balance short-term wins with measures that show lasting performance. Share results so people can act, then use ADRI to strengthen how one key outcome is tracked and reviewed.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Recommendation",
                table: "Categories");
        }
    }
}

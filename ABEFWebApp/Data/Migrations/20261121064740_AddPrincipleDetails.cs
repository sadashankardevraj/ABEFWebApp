using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABEFWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPrincipleDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Explanation",
                table: "Principles",
                type: "nvarchar(600)",
                maxLength: 600,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InPractice",
                table: "Principles",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StrengthSigns",
                table: "Principles",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WeaknessSigns",
                table: "Principles",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Principles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Explanation", "InPractice", "StrengthSigns", "WeaknessSigns" },
                values: new object[] { "People do their best work when they know where the organisation is headed and how their role fits in. Clear direction reduces mixed messages and helps teams make consistent choices day to day.", "Leaders explain purpose and priorities in plain language.|Team goals clearly connect to organisation-wide aims.|People can describe how their work supports the direction.", "Plans and conversations use the same priorities.|Cross-team handoffs feel coordinated.|New starters quickly understand what matters most.", "Different units chase conflicting targets.|People are unsure what to prioritise.|Strategy language stays at the top and never reaches the front line." });

            migrationBuilder.UpdateData(
                table: "Principles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Explanation", "InPractice", "StrengthSigns", "WeaknessSigns" },
                values: new object[] { "Good governance keeps decisions fair, transparent and accountable. Ethical practice protects trust with staff, customers and the wider community.", "Roles, authorities and escalation paths are clear.|Conflicts of interest are declared and managed.|Policies are applied evenly, not only when convenient.", "People raise concerns without fear.|Decisions can be explained and traced.|Leaders model the standards they expect of others.", "Rules bend for favoured people or projects.|Issues are hidden rather than discussed.|Accountability is unclear when something goes wrong." });

            migrationBuilder.UpdateData(
                table: "Principles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Explanation", "InPractice", "StrengthSigns", "WeaknessSigns" },
                values: new object[] { "Organisations succeed when they understand who they serve and what those people value. Focusing on value keeps improvement work connected to real needs, not internal habits alone.", "Customer and stakeholder needs are reviewed regularly.|Services and products are shaped around what users value.|Feedback leads to visible changes.", "Teams talk about value from the user’s point of view.|Complaints and compliments both drive action.|Partners feel their needs are taken seriously.", "Work is designed around internal convenience.|Feedback is collected but rarely used.|Key stakeholders are missing from planning conversations." });

            migrationBuilder.UpdateData(
                table: "Principles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Explanation", "InPractice", "StrengthSigns", "WeaknessSigns" },
                values: new object[] { "Results come from how people, processes and partners work together, not from isolated fixes. A systems view helps teams spot handoffs, delays and unintended effects before they grow.", "End-to-end processes are mapped across team boundaries.|Leaders look at knock-on effects before changing one area.|Shared measures show how the whole system performs.", "Teams cooperate on shared outcomes.|Bottlenecks at handoffs are discussed openly.|Local improvements do not create problems elsewhere.", "Each unit optimises itself and ignores the whole.|Problems bounce between teams with no owner.|Changes in one area surprise another." });

            migrationBuilder.UpdateData(
                table: "Principles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Explanation", "InPractice", "StrengthSigns", "WeaknessSigns" },
                values: new object[] { "Performance depends on people who feel respected, capable and involved. When people are valued, ideas surface earlier and effort stretches further.", "People are involved in decisions that affect their work.|Development and recognition are part of normal management.|Workload and wellbeing are watched, not ignored.", "Staff speak up with ideas and concerns.|People stay and grow in their roles.|Leaders listen as much as they direct.", "Burnout and turnover are treated as normal.|Ideas only come from the top.|Recognition is rare or uneven." });

            migrationBuilder.UpdateData(
                table: "Principles",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Explanation", "InPractice", "StrengthSigns", "WeaknessSigns" },
                values: new object[] { "Strong organisations treat every result as a chance to learn. Learning turns experience into better practice instead of repeating the same mistakes.", "Teams pause to review what worked and what did not.|Lessons are shared beyond the original project.|Experiments are encouraged when the risk is managed.", "People admit mistakes and extract learning.|Successful practices spread quickly.|Reviews lead to real changes in process or skill.", "The same issues appear again and again.|Learning stays locked in one team.|Busy work crowds out reflection time." });

            migrationBuilder.UpdateData(
                table: "Principles",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Explanation", "InPractice", "StrengthSigns", "WeaknessSigns" },
                values: new object[] { "Opinion alone is a weak guide when stakes are high. Using reliable facts helps leaders choose wisely and explain decisions with confidence.", "Key decisions start with clear questions and relevant data.|Measures are understood and trusted by the people who use them.|Evidence is checked before big commitments are made.", "Leaders ask for evidence, not only stories.|Data quality issues are fixed promptly.|Trends are reviewed before reacting to one data point.", "Reports are ignored or disputed without review.|Decisions swing with the loudest voice.|Measures change so often that trends are meaningless." });

            migrationBuilder.UpdateData(
                table: "Principles",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Explanation", "InPractice", "StrengthSigns", "WeaknessSigns" },
                values: new object[] { "Organisations sit inside a wider community and environment. Contributing beyond immediate self-interest builds trust and supports long-term sustainability.", "Community and environmental impacts are considered in planning.|Local partnerships support shared goals.|People are encouraged to contribute beyond their day job where it fits.", "External impact is discussed alongside financial results.|Partners see the organisation as a good neighbour.|Commitments to the community are kept over time.", "Wider impacts are treated as optional extras.|Short-term gain overrides community harm.|Promises to partners are forgotten once pressure rises." });

            migrationBuilder.UpdateData(
                table: "Principles",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Explanation", "InPractice", "StrengthSigns", "WeaknessSigns" },
                values: new object[] { "Leaders shape what people believe is possible. Visionary leadership looks ahead, builds confidence and keeps improvement moving when conditions change.", "Leaders describe a clear future people can believe in.|They model the behaviours they want others to follow.|They keep focus on long-term capability as well as short-term delivery.", "People feel motivated by the future direction.|Leaders stay visible during change and challenge.|Ambition is matched with practical next steps.", "Vision statements feel empty or unused.|Leaders only react to urgent issues.|People wait for permission instead of taking initiative." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Explanation",
                table: "Principles");

            migrationBuilder.DropColumn(
                name: "InPractice",
                table: "Principles");

            migrationBuilder.DropColumn(
                name: "StrengthSigns",
                table: "Principles");

            migrationBuilder.DropColumn(
                name: "WeaknessSigns",
                table: "Principles");
        }
    }
}

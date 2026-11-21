using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ABEFWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAbefFrameworkData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Principles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Principles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Number", "ShortDescription", "Type" },
                values: new object[,]
                {
                    { 1, "Leadership, Culture and Governance", 1, "Sets direction, culture and good governance.", "Driver" },
                    { 2, "Customers and other Stakeholders", 2, "Understands and delivers value to customers and other stakeholders.", "Driver" },
                    { 3, "Strategy and Planning", 3, "Turns direction into clear plans and goals.", "Enabler" },
                    { 4, "People", 4, "Develops, supports and engages the workforce.", "Enabler" },
                    { 5, "Information and Knowledge", 5, "Uses data and knowledge to make better decisions.", "Enabler" },
                    { 6, "Process Management, Improvement and Innovation", 6, "Designs, improves and innovates how work gets done.", "Enabler" },
                    { 7, "Results and Sustainable Performance", 7, "Measures outcomes and long-term performance.", "Result" }
                });

            migrationBuilder.InsertData(
                table: "Principles",
                columns: new[] { "Id", "Name", "Number", "ShortDescription" },
                values: new object[,]
                {
                    { 1, "Clear Direction and Organisational Alignment", 1, "Shared direction helps everyone pull toward the same goals." },
                    { 2, "Ethical Governance", 2, "Honest, responsible governance builds trust and good decisions." },
                    { 3, "Customer and Stakeholder Value", 3, "Excellence starts with creating value for customers and stakeholders." },
                    { 4, "A Systems Perspective", 4, "The organisation works best when its parts are managed as one system." },
                    { 5, "Valuing People", 5, "People’s skill, energy and involvement lift organisational performance." },
                    { 6, "Organisational Learning", 6, "Learning and improvement keep the organisation adaptable." },
                    { 7, "Management by Fact", 7, "Good decisions are based on reliable facts and evidence." },
                    { 8, "Societal Contributions", 8, "Organisations create value for the wider community, not only themselves." },
                    { 9, "Visionary Leadership", 9, "Leaders set the tone and inspire a future-focused culture." }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CategoryId", "Code", "Name", "ShortDescription" },
                values: new object[,]
                {
                    { 1, 1, "1.1", "Leadership and governance throughout the organisation", "Leaders set clear expectations and govern the whole organisation well." },
                    { 2, 1, "1.2", "Leading the organisational culture", "Leaders shape a culture that supports excellence every day." },
                    { 3, 1, "1.3", "Purpose and Vision", "The organisation is guided by a clear purpose and vision." },
                    { 4, 1, "1.4", "Society, community and environmental responsibility", "The organisation acts responsibly toward society, community and the environment." },
                    { 5, 2, "2.1", "Knowledge of markets, customers and other stakeholders", "The organisation understands its markets, customers and other stakeholders." },
                    { 6, 2, "2.2", "Effective management of customer and other stakeholder relationships", "Relationships with customers and stakeholders are managed carefully and well." },
                    { 7, 2, "2.3", "Measuring customers and other stakeholders' perception of value", "The organisation checks how customers and stakeholders judge the value they receive." },
                    { 8, 3, "3.1", "Understand the ecosystem and organisational capability", "Leaders understand the wider environment and what the organisation can do." },
                    { 9, 3, "3.2", "Understand stakeholder requirements", "Plans are shaped by what stakeholders need and expect." },
                    { 10, 3, "3.3", "The planning process", "Planning turns direction into practical goals and actions." },
                    { 11, 4, "4.1", "Organisational capability through people", "People capability is built to meet the organisation’s goals." },
                    { 12, 4, "4.2", "People effectiveness and development", "People are supported to grow and perform effectively." },
                    { 13, 4, "4.3", "A great place to work", "The workplace supports wellbeing, engagement and pride in work." },
                    { 14, 5, "5.1", "Identifying, generating, collecting and analysing the right data to inform decision-making", "The right data is gathered and analysed to guide decisions." },
                    { 15, 5, "5.2", "Creating value through applying knowledge", "Knowledge is used in ways that create real value." },
                    { 16, 5, "5.3", "Enable innovation and the use of technology", "Technology and new ideas are used to improve how work is done." },
                    { 17, 6, "6.1", "Identification and management of organisational processes", "Key processes are identified, owned and managed clearly." },
                    { 18, 6, "6.2", "Management of supplier and stakeholder processes", "Processes with suppliers and stakeholders are managed effectively." },
                    { 19, 6, "6.3", "Process improvement and innovation", "Processes are improved and innovated over time." },
                    { 20, 7, "7.1", "Measuring and communicating organisational results", "Results are measured and shared so people know how the organisation is performing." },
                    { 21, 7, "7.2", "Achieving sustainable performance", "Performance is sustained for the long term, not only short-term gains." }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Number",
                table: "Categories",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_CategoryId",
                table: "Items",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Code",
                table: "Items",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Principles_Number",
                table: "Principles",
                column: "Number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Principles");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}

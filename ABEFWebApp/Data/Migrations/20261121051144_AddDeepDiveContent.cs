using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABEFWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDeepDiveContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApproachPoints",
                table: "Items",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeploymentPoints",
                table: "Items",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImprovementPoints",
                table: "Items",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReflectionQuestion",
                table: "Items",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResultsPoints",
                table: "Items",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhyItMatters",
                table: "Items",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Purpose",
                table: "Categories",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tips",
                table: "Categories",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Purpose", "Tips" },
                values: new object[] { "This category looks at how leaders set direction, model behaviour and oversee the organisation. It also covers culture and the systems that keep decisions honest and accountable.", "Ask leaders how they show the values day to day.|Check that governance roles and decisions are clear.|Look for evidence that culture supports the stated purpose." });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Purpose", "Tips" },
                values: new object[] { "This category focuses on knowing who you serve and what they value. It covers how relationships are managed and how feedback shapes better outcomes.", "Map who your main customers and stakeholders are.|Review how often you listen to their feedback.|Check whether value is defined in their terms, not only yours." });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Purpose", "Tips" },
                values: new object[] { "This category is about turning direction into practical plans. It links the wider environment, stakeholder needs and the organisation’s ability to deliver.", "Test whether plans reflect real external pressures.|Confirm goals cascade into team actions.|Review progress on a fixed cycle, not only at year end." });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Purpose", "Tips" },
                values: new object[] { "This category covers how the organisation builds capability through its people. It includes development, effectiveness and creating a workplace people want to stay in.", "Link people plans to strategy, not only to HR routines.|Ask teams what helps or blocks their best work.|Track engagement and wellbeing alongside performance." });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Purpose", "Tips" },
                values: new object[] { "This category looks at how data and knowledge support better decisions. It also covers sharing learning and using technology to create value.", "Confirm leaders use the same core measures.|Separate noise from the data that really matters.|Make knowledge easy to find and reuse across teams." });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Purpose", "Tips" },
                values: new object[] { "This category is about how work flows through clear, managed processes. It includes suppliers and partners, and how processes are improved or reinvented over time.", "Name process owners for the work that matters most.|Map handoffs where delays or defects appear.|Protect time for both small fixes and bigger innovation." });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Purpose", "Tips" },
                values: new object[] { "This category examines the results the organisation achieves and whether they hold up over time. It links measures, communication and long-term sustainability.", "Balance short-term wins with longer-term indicators.|Share results in a way people can act on.|Ask whether today’s success could still be true in three years." });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ApproachPoints", "DeploymentPoints", "ImprovementPoints", "ReflectionQuestion", "ResultsPoints", "WhyItMatters" },
                values: new object[] { "How do we define leadership roles and decision rights?|What governance structures guide major choices?|How do leaders set expectations for behaviour and performance?", "Are leadership practices consistent across all levels?|Is governance applied in every business unit?|Do frontline teams know who decides what?", "How do we review leadership effectiveness?|When do we refresh governance rules or structures?|How do lessons from poor decisions feed back into practice?", "If a new leader joined tomorrow, how quickly could they see how decisions are made here?", "What shows that governance decisions are timely and sound?|How do we know leadership expectations are understood?|What evidence shows accountability is working in practice?", "Clear leadership and governance keep decisions consistent and accountable. Without them, teams can pull in different directions and risk goes unmanaged." });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ApproachPoints", "DeploymentPoints", "ImprovementPoints", "ReflectionQuestion", "ResultsPoints", "WhyItMatters" },
                values: new object[] { "What culture do we intentionally want to build?|How do leaders plan to model those behaviours?|How do we reinforce culture through recognition and stories?", "Is the desired culture visible in every team?|Do leaders act the same way in pressure situations?|Are culture messages consistent across sites and roles?", "How often do we check whether culture matches intent?|What do we change when culture gaps appear?|How do new starters learn the culture we want?", "Would a visitor describe our culture the same way our leaders do?", "What feedback tells us culture is healthy?|How do we see culture showing up in day-to-day work?|What happens when behaviour does not match our values?", "Culture shapes how people behave when no one is watching. Leaders who model the right culture make excellence easier to sustain." });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ApproachPoints", "DeploymentPoints", "ImprovementPoints", "ReflectionQuestion", "ResultsPoints", "WhyItMatters" },
                values: new object[] { "How was our purpose and vision defined?|Who was involved in shaping them?|How do they connect to strategy and daily priorities?", "Do all teams know and use the purpose and vision?|Are they referenced in planning and decisions?|Are they communicated in plain language across the organisation?", "When do we review whether purpose and vision still fit?|How do we update them as the organisation changes?|How do we test understanding with people at every level?", "Can every team explain how their work supports our purpose?", "What shows people understand our purpose?|How do we know the vision guides real choices?|What evidence links purpose to engagement or outcomes?", "Purpose and vision give people a shared reason to act. When they are clear, priorities and trade-offs become easier to explain." });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ApproachPoints", "DeploymentPoints", "ImprovementPoints", "ReflectionQuestion", "ResultsPoints", "WhyItMatters" },
                values: new object[] { "How do we define our social and environmental responsibilities?|What commitments guide community and environmental work?|How do these responsibilities link to our purpose?", "Are responsibility practices applied across sites and teams?|Do suppliers and partners follow similar expectations?|Is community involvement part of normal operations?", "How do we review social and environmental performance?|What triggers a change in our responsibility approach?|How do we learn from community feedback?", "If our neighbours described us, would they say we leave the place better than we found it?", "What measures show our community or environmental impact?|How do stakeholders rate our responsibility?|What evidence shows we keep our commitments?", "Responsible organisations earn trust beyond their immediate customers. Community and environmental care also reduce long-term risk." });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ApproachPoints", "DeploymentPoints", "ImprovementPoints", "ReflectionQuestion", "ResultsPoints", "WhyItMatters" },
                values: new object[] { "How do we identify our markets, customers and stakeholders?|What methods do we use to learn their needs?|How often do we refresh that knowledge?", "Is market and stakeholder insight shared with the teams who need it?|Do all relevant units use the same customer picture?|Are insights updated before major decisions?", "How do we improve the way we gather market knowledge?|When do we challenge outdated assumptions?|How do we close gaps in stakeholder understanding?", "What would surprise us if we asked our customers and stakeholders today?", "What shows our understanding of customers is accurate?|How do we know we have not missed key stakeholders?|What evidence links insight to better offers or services?", "You cannot create value without knowing who you serve and what they need. Market and stakeholder insight keeps plans grounded in reality." });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ApproachPoints", "DeploymentPoints", "ImprovementPoints", "ReflectionQuestion", "ResultsPoints", "WhyItMatters" },
                values: new object[] { "How do we design our approach to managing key relationships?|Who owns each major customer or stakeholder relationship?|What standards guide how we communicate and follow up?", "Are relationship practices used consistently across teams?|Do partners and frontline staff follow the same approach?|Is escalation clear when a relationship is at risk?", "How do we review relationship performance?|What do we change after a relationship fails?|How do we share good relationship practice across teams?", "Who would notice first if one of our key relationships started to weaken?", "What shows relationships are healthy and lasting?|How do we track complaints, renewals or referrals?|What feedback tells us stakeholders feel well looked after?", "Strong relationships turn one-off transactions into lasting trust. Clear ownership of those relationships reduces friction and missed opportunities." });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ApproachPoints", "DeploymentPoints", "ImprovementPoints", "ReflectionQuestion", "ResultsPoints", "WhyItMatters" },
                values: new object[] { "How do we define value from the customer and stakeholder view?|What methods do we use to measure their perception?|How do findings feed into planning and improvement?", "Do we measure perception across all key customer groups?|Are results shared with the teams who can act?|Is listening continuous, not only an annual survey?", "How do we improve the questions we ask about value?|When do we change how we collect perception data?|How do we close the loop with people who gave feedback?", "Are we measuring what customers value, or only what is easy for us to count?", "What trends show in perception of value over time?|How do scores differ by customer or stakeholder group?|What evidence links perception work to service changes?", "Value is judged by the people who receive it, not only by internal measures. Listening to that perception helps you improve what matters most." });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ApproachPoints", "DeploymentPoints", "ImprovementPoints", "ReflectionQuestion", "ResultsPoints", "WhyItMatters" },
                values: new object[] { "How do we scan the wider ecosystem for opportunities and risks?|How do we assess our current organisational capability?|How do ecosystem and capability views shape strategy?", "Is ecosystem scanning used across planning teams?|Do capability assessments cover all critical functions?|Are insights available when plans are set or revised?", "How often do we refresh ecosystem and capability reviews?|What do we change when the environment shifts quickly?|How do we improve the quality of capability assessment?", "Where are we strongest, and where is the outside world moving faster than we are?", "What shows our ecosystem view is current and useful?|How do we know capability gaps are correctly identified?|What evidence links this understanding to better plans?", "Strategy fails when it ignores the outside world or overstates internal strength. Knowing both helps you choose goals you can actually deliver." });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ApproachPoints", "DeploymentPoints", "ImprovementPoints", "ReflectionQuestion", "ResultsPoints", "WhyItMatters" },
                values: new object[] { "How do we gather stakeholder requirements for planning?|How do we weigh competing needs and expectations?|How do requirements translate into goals and actions?", "Are stakeholder requirements used in every major plan?|Do teams revisit requirements during delivery?|Is the same requirement set shared across planning groups?", "How do we improve the way we capture requirements?|When do we reopen plans after requirements change?|How do we learn from plans that missed the mark?", "Which stakeholder need would hurt us most if we got it wrong?", "What shows we understood requirements correctly?|How often do plans miss a key stakeholder need?|What feedback confirms stakeholders feel heard in planning?", "Plans that ignore stakeholder needs create friction later. Understanding requirements early helps set priorities that people will support." });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ApproachPoints", "DeploymentPoints", "ImprovementPoints", "ReflectionQuestion", "ResultsPoints", "WhyItMatters" },
                values: new object[] { "How is our planning process designed end to end?|Who is involved and how are priorities set?|How do plans connect to measures and resources?", "Is the same planning process used across units?|Do teams understand their part in the plan?|Are plans updated when conditions change?", "How do we review the planning process itself?|What do we simplify when planning feels too heavy?|How do we capture lessons after each planning cycle?", "Can a team member point to the plan that guides their week?", "What shows plans are delivered on time and on target?|How do we know goals cascade into team actions?|What evidence shows planning improves results?", "A clear planning process turns ambition into work people can do. Without it, goals stay vague and progress is hard to track." });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ApproachPoints", "DeploymentPoints", "ImprovementPoints", "ReflectionQuestion", "ResultsPoints", "WhyItMatters" },
                values: new object[] { "How do we identify the capabilities we need for our goals?|How do we plan to build or hire those capabilities?|How do people plans link to strategy?", "Are capability plans applied across all critical roles?|Do teams know the skills they are expected to grow?|Is capability building consistent across locations?", "How do we review workforce capability against strategy?|What do we change when new skills become essential?|How do we learn from roles that are hard to fill or retain?", "Which capability, if missing next year, would put our goals at risk?", "What shows we have the people capability we need?|Where do skill gaps still slow delivery?|What evidence links people investment to performance?", "Strategy only works if people have the skills to deliver it. Building capability on purpose reduces reliance on a few key individuals." });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ApproachPoints", "DeploymentPoints", "ImprovementPoints", "ReflectionQuestion", "ResultsPoints", "WhyItMatters" },
                values: new object[] { "How do we set expectations for effective performance?|What development pathways do we offer?|How do managers support learning on the job?", "Are development practices available to all employees?|Do managers apply coaching and feedback consistently?|Is development linked to real work, not only courses?", "How do we improve development offerings over time?|When do we adjust approaches that are not working?|How do we share effective coaching practices?", "Would most people say they are growing here, or just getting through the week?", "What shows people are becoming more effective?|How do we track development progress and outcomes?|What feedback tells us support is working?", "People perform better when expectations are clear and development is real. Growth support also helps retain the talent you need." });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "ApproachPoints", "DeploymentPoints", "ImprovementPoints", "ReflectionQuestion", "ResultsPoints", "WhyItMatters" },
                values: new object[] { "How do we define what a great place to work means here?|What practices support wellbeing and engagement?|How do leaders plan to build pride in the work?", "Are wellbeing and engagement practices felt in every team?|Do remote and on-site staff get equitable support?|Is recognition used fairly across the organisation?", "How do we act on engagement survey findings?|What workplace issues do we fix first?|How do we keep improving the employee experience?", "Would you recommend this workplace to someone you care about?", "What engagement or wellbeing measures do we track?|How do retention and referral trends look?|What stories show people feel proud to work here?", "A workplace people value attracts and keeps talent. Wellbeing and pride also lift day-to-day quality and collaboration." });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ApproachPoints", "DeploymentPoints", "ImprovementPoints", "ReflectionQuestion", "ResultsPoints", "WhyItMatters" },
                values: new object[] { "How do we decide which data matters for decisions?|How do we collect and quality-check that data?|How is analysis designed to support leaders?", "Do decision-makers get timely access to the right data?|Is data practice consistent across teams?|Are definitions shared so measures mean the same thing?", "How do we review whether we still measure the right things?|What data do we stop collecting because it adds noise?|How do we improve analysis skills across the organisation?", "Which report would we miss least if it disappeared tomorrow?", "What shows decisions are better because of our data?|How often is key data late, incomplete or unused?|What evidence shows analysis quality is improving?", "Decisions are only as good as the information behind them. Focusing on the right data saves time and reduces guesswork." });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "ApproachPoints", "DeploymentPoints", "ImprovementPoints", "ReflectionQuestion", "ResultsPoints", "WhyItMatters" },
                values: new object[] { "How do we capture knowledge that creates value?|How do people find and apply what others have learned?|How do we encourage knowledge sharing as normal work?", "Is useful knowledge available across teams and sites?|Do people reuse lessons before starting new work?|Are knowledge tools part of everyday processes?", "How do we improve knowledge capture and access?|What barriers to sharing do we remove?|How do we refresh knowledge that has become outdated?", "Where does important know-how still live only in one person’s notebook or head?", "What shows knowledge reuse is creating value?|How do we know critical know-how is not locked in a few heads?|What examples prove shared learning improved outcomes?", "Stored knowledge has little worth until people apply it. Sharing and using know-how helps teams avoid repeating mistakes." });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "ApproachPoints", "DeploymentPoints", "ImprovementPoints", "ReflectionQuestion", "ResultsPoints", "WhyItMatters" },
                values: new object[] { "How do we decide where innovation and technology will help most?|What process turns ideas into tested changes?|How do we choose and adopt technology thoughtfully?", "Are innovation practices open to people beyond one team?|Is useful technology rolled out consistently?|Do teams get support to adopt new tools well?", "How do we review failed or stalled innovations?|What do we change in our technology choices over time?|How do we keep the innovation pipeline healthy?", "Are we adopting technology because it helps people, or because it is new?", "What shows innovation is delivering real benefits?|How do we measure technology adoption and value?|What evidence shows ideas move from concept to use?", "Technology and innovation help the organisation stay relevant. Used well, they free people for higher-value work and better services." });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "ApproachPoints", "DeploymentPoints", "ImprovementPoints", "ReflectionQuestion", "ResultsPoints", "WhyItMatters" },
                values: new object[] { "How do we identify the processes that matter most?|Who owns each key process and what are they accountable for?|How are process standards and measures defined?", "Are key processes documented and used in daily work?|Is ownership clear across all units involved?|Do handoffs between teams follow agreed steps?", "How do we review process performance and ownership?|When do we redesign a process that no longer fits?|How do we learn from process breakdowns?", "If a key process failed tomorrow, who would own fixing it?", "What process measures show stability or problems?|How often do process failures cause customer issues?|What evidence shows process ownership is working?", "Clear process ownership reduces waste and confusion. When key work is managed well, quality and speed become more predictable." });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "ApproachPoints", "DeploymentPoints", "ImprovementPoints", "ReflectionQuestion", "ResultsPoints", "WhyItMatters" },
                values: new object[] { "How do we define processes that involve suppliers and stakeholders?|What expectations and standards guide those partnerships?|How do we manage risk in shared processes?", "Are supplier and stakeholder processes applied consistently?|Do partners understand their role in the process?|Is performance reviewed with suppliers, not only internally?", "How do we improve shared processes with partners?|When do we change a supplier arrangement that is not working?|How do we share learning across partner relationships?", "Which supplier or partner process would cause the biggest problem if it slipped?", "What shows supplier and partner processes deliver as expected?|How do we track quality, timeliness or issues from partners?|What feedback do stakeholders give on working with us?", "Much of your performance depends on partners outside your walls. Managing those shared processes well protects quality and relationships." });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "ApproachPoints", "DeploymentPoints", "ImprovementPoints", "ReflectionQuestion", "ResultsPoints", "WhyItMatters" },
                values: new object[] { "How do we decide which processes to improve or reinvent?|What methods guide improvement and innovation work?|How do people raise and test process ideas?", "Is improvement activity happening across the organisation?|Do teams have time and tools to improve their processes?|Are successful changes spread beyond the first team?", "How do we review our improvement approach itself?|What blocks people from improving processes?|How do we keep momentum when early wins fade?", "When did we last make a process simpler rather than add another step?", "What gains have process improvements delivered?|How many improvements move from idea to lasting change?|What evidence shows innovation in processes, not only small fixes?", "Even good processes drift or become outdated. Regular improvement and occasional reinvention keep work efficient and relevant." });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "ApproachPoints", "DeploymentPoints", "ImprovementPoints", "ReflectionQuestion", "ResultsPoints", "WhyItMatters" },
                values: new object[] { "How do we choose the results that matter most?|How are results measured and validated?|How do we plan to communicate results to different audiences?", "Are key results visible to the people who can act on them?|Is communication timely and consistent across units?|Do leaders discuss results with their teams regularly?", "How do we improve the measures we use over time?|When do we change how results are shared?|How do we learn from results that surprised us?", "Could most people explain our three most important results without looking them up?", "What shows our measures give a true performance picture?|How well do people understand current results?|What evidence links result communication to better action?", "People cannot improve what they cannot see. Clear measures and honest communication keep everyone aligned on performance." });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "ApproachPoints", "DeploymentPoints", "ImprovementPoints", "ReflectionQuestion", "ResultsPoints", "WhyItMatters" },
                values: new object[] { "How do we define sustainable performance for our organisation?|What long-term indicators sit alongside short-term targets?|How do we plan to protect capability while delivering results?", "Are sustainable-performance practices used across the organisation?|Do teams balance delivery pressure with long-term health?|Are long-term indicators reviewed as often as short-term ones?", "How do we review whether performance is truly sustainable?|What do we change when gains start to fade?|How do we learn from periods of strong and weak sustainability?", "Are today’s results building the organisation we will need in three years?", "What trends show performance is holding over time?|Where have short-term gains created later problems?|What evidence shows we can sustain results under pressure?", "Short-term wins mean little if they cannot be sustained. Lasting performance balances today’s results with tomorrow’s capacity." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApproachPoints",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "DeploymentPoints",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ImprovementPoints",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ReflectionQuestion",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ResultsPoints",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "WhyItMatters",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Purpose",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Tips",
                table: "Categories");
        }
    }
}

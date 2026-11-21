using ABEFWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ABEFWebApp.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<ContactMessage> ContactMessages => Set<ContactMessage>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Item> Items => Set<Item>();
    public DbSet<Principle> Principles => Set<Principle>();
    public DbSet<Event> Events => Set<Event>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Category>(entity =>
        {
            entity.HasIndex(c => c.Number).IsUnique();
            entity.HasMany(c => c.Items)
                .WithOne(i => i.Category!)
                .HasForeignKey(i => i.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Item>(entity =>
        {
            entity.HasIndex(i => i.Code).IsUnique();
        });

        builder.Entity<Principle>(entity =>
        {
            entity.HasIndex(p => p.Number).IsUnique();
        });

        builder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                Number = 1,
                Name = "Leadership, Culture and Governance",
                Type = "Driver",
                ShortDescription = "Sets direction, culture and good governance.",
                Purpose = "This category looks at how leaders set direction, model behaviour and oversee the organisation. It also covers culture and the systems that keep decisions honest and accountable.",
                Tips = "Ask leaders how they show the values day to day.|Check that governance roles and decisions are clear.|Look for evidence that culture supports the stated purpose."
            },
            new Category
            {
                Id = 2,
                Number = 2,
                Name = "Customers and other Stakeholders",
                Type = "Driver",
                ShortDescription = "Understands and delivers value to customers and other stakeholders.",
                Purpose = "This category focuses on knowing who you serve and what they value. It covers how relationships are managed and how feedback shapes better outcomes.",
                Tips = "Map who your main customers and stakeholders are.|Review how often you listen to their feedback.|Check whether value is defined in their terms, not only yours."
            },
            new Category
            {
                Id = 3,
                Number = 3,
                Name = "Strategy and Planning",
                Type = "Enabler",
                ShortDescription = "Turns direction into clear plans and goals.",
                Purpose = "This category is about turning direction into practical plans. It links the wider environment, stakeholder needs and the organisation’s ability to deliver.",
                Tips = "Test whether plans reflect real external pressures.|Confirm goals cascade into team actions.|Review progress on a fixed cycle, not only at year end."
            },
            new Category
            {
                Id = 4,
                Number = 4,
                Name = "People",
                Type = "Enabler",
                ShortDescription = "Develops, supports and engages the workforce.",
                Purpose = "This category covers how the organisation builds capability through its people. It includes development, effectiveness and creating a workplace people want to stay in.",
                Tips = "Link people plans to strategy, not only to HR routines.|Ask teams what helps or blocks their best work.|Track engagement and wellbeing alongside performance."
            },
            new Category
            {
                Id = 5,
                Number = 5,
                Name = "Information and Knowledge",
                Type = "Enabler",
                ShortDescription = "Uses data and knowledge to make better decisions.",
                Purpose = "This category looks at how data and knowledge support better decisions. It also covers sharing learning and using technology to create value.",
                Tips = "Confirm leaders use the same core measures.|Separate noise from the data that really matters.|Make knowledge easy to find and reuse across teams."
            },
            new Category
            {
                Id = 6,
                Number = 6,
                Name = "Process Management, Improvement and Innovation",
                Type = "Enabler",
                ShortDescription = "Designs, improves and innovates how work gets done.",
                Purpose = "This category is about how work flows through clear, managed processes. It includes suppliers and partners, and how processes are improved or reinvented over time.",
                Tips = "Name process owners for the work that matters most.|Map handoffs where delays or defects appear.|Protect time for both small fixes and bigger innovation."
            },
            new Category
            {
                Id = 7,
                Number = 7,
                Name = "Results and Sustainable Performance",
                Type = "Result",
                ShortDescription = "Measures outcomes and long-term performance.",
                Purpose = "This category examines the results the organisation achieves and whether they hold up over time. It links measures, communication and long-term sustainability.",
                Tips = "Balance short-term wins with longer-term indicators.|Share results in a way people can act on.|Ask whether today’s success could still be true in three years."
            });

        builder.Entity<Item>().HasData(
            new Item
            {
                Id = 1,
                Code = "1.1",
                Name = "Leadership and governance throughout the organisation",
                ShortDescription = "Leaders set clear expectations and govern the whole organisation well.",
                CategoryId = 1,
                WhyItMatters = "Clear leadership and governance keep decisions consistent and accountable. Without them, teams can pull in different directions and risk goes unmanaged.",
                ApproachPoints = "How do we define leadership roles and decision rights?|What governance structures guide major choices?|How do leaders set expectations for behaviour and performance?",
                DeploymentPoints = "Are leadership practices consistent across all levels?|Is governance applied in every business unit?|Do frontline teams know who decides what?",
                ResultsPoints = "What shows that governance decisions are timely and sound?|How do we know leadership expectations are understood?|What evidence shows accountability is working in practice?",
                ImprovementPoints = "How do we review leadership effectiveness?|When do we refresh governance rules or structures?|How do lessons from poor decisions feed back into practice?",
                ReflectionQuestion = "If a new leader joined tomorrow, how quickly could they see how decisions are made here?"
            },
            new Item
            {
                Id = 2,
                Code = "1.2",
                Name = "Leading the organisational culture",
                ShortDescription = "Leaders shape a culture that supports excellence every day.",
                CategoryId = 1,
                WhyItMatters = "Culture shapes how people behave when no one is watching. Leaders who model the right culture make excellence easier to sustain.",
                ApproachPoints = "What culture do we intentionally want to build?|How do leaders plan to model those behaviours?|How do we reinforce culture through recognition and stories?",
                DeploymentPoints = "Is the desired culture visible in every team?|Do leaders act the same way in pressure situations?|Are culture messages consistent across sites and roles?",
                ResultsPoints = "What feedback tells us culture is healthy?|How do we see culture showing up in day-to-day work?|What happens when behaviour does not match our values?",
                ImprovementPoints = "How often do we check whether culture matches intent?|What do we change when culture gaps appear?|How do new starters learn the culture we want?",
                ReflectionQuestion = "Would a visitor describe our culture the same way our leaders do?"
            },
            new Item
            {
                Id = 3,
                Code = "1.3",
                Name = "Purpose and Vision",
                ShortDescription = "The organisation is guided by a clear purpose and vision.",
                CategoryId = 1,
                WhyItMatters = "Purpose and vision give people a shared reason to act. When they are clear, priorities and trade-offs become easier to explain.",
                ApproachPoints = "How was our purpose and vision defined?|Who was involved in shaping them?|How do they connect to strategy and daily priorities?",
                DeploymentPoints = "Do all teams know and use the purpose and vision?|Are they referenced in planning and decisions?|Are they communicated in plain language across the organisation?",
                ResultsPoints = "What shows people understand our purpose?|How do we know the vision guides real choices?|What evidence links purpose to engagement or outcomes?",
                ImprovementPoints = "When do we review whether purpose and vision still fit?|How do we update them as the organisation changes?|How do we test understanding with people at every level?",
                ReflectionQuestion = "Can every team explain how their work supports our purpose?"
            },
            new Item
            {
                Id = 4,
                Code = "1.4",
                Name = "Society, community and environmental responsibility",
                ShortDescription = "The organisation acts responsibly toward society, community and the environment.",
                CategoryId = 1,
                WhyItMatters = "Responsible organisations earn trust beyond their immediate customers. Community and environmental care also reduce long-term risk.",
                ApproachPoints = "How do we define our social and environmental responsibilities?|What commitments guide community and environmental work?|How do these responsibilities link to our purpose?",
                DeploymentPoints = "Are responsibility practices applied across sites and teams?|Do suppliers and partners follow similar expectations?|Is community involvement part of normal operations?",
                ResultsPoints = "What measures show our community or environmental impact?|How do stakeholders rate our responsibility?|What evidence shows we keep our commitments?",
                ImprovementPoints = "How do we review social and environmental performance?|What triggers a change in our responsibility approach?|How do we learn from community feedback?",
                ReflectionQuestion = "If our neighbours described us, would they say we leave the place better than we found it?"
            },
            new Item
            {
                Id = 5,
                Code = "2.1",
                Name = "Knowledge of markets, customers and other stakeholders",
                ShortDescription = "The organisation understands its markets, customers and other stakeholders.",
                CategoryId = 2,
                WhyItMatters = "You cannot create value without knowing who you serve and what they need. Market and stakeholder insight keeps plans grounded in reality.",
                ApproachPoints = "How do we identify our markets, customers and stakeholders?|What methods do we use to learn their needs?|How often do we refresh that knowledge?",
                DeploymentPoints = "Is market and stakeholder insight shared with the teams who need it?|Do all relevant units use the same customer picture?|Are insights updated before major decisions?",
                ResultsPoints = "What shows our understanding of customers is accurate?|How do we know we have not missed key stakeholders?|What evidence links insight to better offers or services?",
                ImprovementPoints = "How do we improve the way we gather market knowledge?|When do we challenge outdated assumptions?|How do we close gaps in stakeholder understanding?",
                ReflectionQuestion = "What would surprise us if we asked our customers and stakeholders today?"
            },
            new Item
            {
                Id = 6,
                Code = "2.2",
                Name = "Effective management of customer and other stakeholder relationships",
                ShortDescription = "Relationships with customers and stakeholders are managed carefully and well.",
                CategoryId = 2,
                WhyItMatters = "Strong relationships turn one-off transactions into lasting trust. Clear ownership of those relationships reduces friction and missed opportunities.",
                ApproachPoints = "How do we design our approach to managing key relationships?|Who owns each major customer or stakeholder relationship?|What standards guide how we communicate and follow up?",
                DeploymentPoints = "Are relationship practices used consistently across teams?|Do partners and frontline staff follow the same approach?|Is escalation clear when a relationship is at risk?",
                ResultsPoints = "What shows relationships are healthy and lasting?|How do we track complaints, renewals or referrals?|What feedback tells us stakeholders feel well looked after?",
                ImprovementPoints = "How do we review relationship performance?|What do we change after a relationship fails?|How do we share good relationship practice across teams?",
                ReflectionQuestion = "Who would notice first if one of our key relationships started to weaken?"
            },
            new Item
            {
                Id = 7,
                Code = "2.3",
                Name = "Measuring customers and other stakeholders' perception of value",
                ShortDescription = "The organisation checks how customers and stakeholders judge the value they receive.",
                CategoryId = 2,
                WhyItMatters = "Value is judged by the people who receive it, not only by internal measures. Listening to that perception helps you improve what matters most.",
                ApproachPoints = "How do we define value from the customer and stakeholder view?|What methods do we use to measure their perception?|How do findings feed into planning and improvement?",
                DeploymentPoints = "Do we measure perception across all key customer groups?|Are results shared with the teams who can act?|Is listening continuous, not only an annual survey?",
                ResultsPoints = "What trends show in perception of value over time?|How do scores differ by customer or stakeholder group?|What evidence links perception work to service changes?",
                ImprovementPoints = "How do we improve the questions we ask about value?|When do we change how we collect perception data?|How do we close the loop with people who gave feedback?",
                ReflectionQuestion = "Are we measuring what customers value, or only what is easy for us to count?"
            },
            new Item
            {
                Id = 8,
                Code = "3.1",
                Name = "Understand the ecosystem and organisational capability",
                ShortDescription = "Leaders understand the wider environment and what the organisation can do.",
                CategoryId = 3,
                WhyItMatters = "Strategy fails when it ignores the outside world or overstates internal strength. Knowing both helps you choose goals you can actually deliver.",
                ApproachPoints = "How do we scan the wider ecosystem for opportunities and risks?|How do we assess our current organisational capability?|How do ecosystem and capability views shape strategy?",
                DeploymentPoints = "Is ecosystem scanning used across planning teams?|Do capability assessments cover all critical functions?|Are insights available when plans are set or revised?",
                ResultsPoints = "What shows our ecosystem view is current and useful?|How do we know capability gaps are correctly identified?|What evidence links this understanding to better plans?",
                ImprovementPoints = "How often do we refresh ecosystem and capability reviews?|What do we change when the environment shifts quickly?|How do we improve the quality of capability assessment?",
                ReflectionQuestion = "Where are we strongest, and where is the outside world moving faster than we are?"
            },
            new Item
            {
                Id = 9,
                Code = "3.2",
                Name = "Understand stakeholder requirements",
                ShortDescription = "Plans are shaped by what stakeholders need and expect.",
                CategoryId = 3,
                WhyItMatters = "Plans that ignore stakeholder needs create friction later. Understanding requirements early helps set priorities that people will support.",
                ApproachPoints = "How do we gather stakeholder requirements for planning?|How do we weigh competing needs and expectations?|How do requirements translate into goals and actions?",
                DeploymentPoints = "Are stakeholder requirements used in every major plan?|Do teams revisit requirements during delivery?|Is the same requirement set shared across planning groups?",
                ResultsPoints = "What shows we understood requirements correctly?|How often do plans miss a key stakeholder need?|What feedback confirms stakeholders feel heard in planning?",
                ImprovementPoints = "How do we improve the way we capture requirements?|When do we reopen plans after requirements change?|How do we learn from plans that missed the mark?",
                ReflectionQuestion = "Which stakeholder need would hurt us most if we got it wrong?"
            },
            new Item
            {
                Id = 10,
                Code = "3.3",
                Name = "The planning process",
                ShortDescription = "Planning turns direction into practical goals and actions.",
                CategoryId = 3,
                WhyItMatters = "A clear planning process turns ambition into work people can do. Without it, goals stay vague and progress is hard to track.",
                ApproachPoints = "How is our planning process designed end to end?|Who is involved and how are priorities set?|How do plans connect to measures and resources?",
                DeploymentPoints = "Is the same planning process used across units?|Do teams understand their part in the plan?|Are plans updated when conditions change?",
                ResultsPoints = "What shows plans are delivered on time and on target?|How do we know goals cascade into team actions?|What evidence shows planning improves results?",
                ImprovementPoints = "How do we review the planning process itself?|What do we simplify when planning feels too heavy?|How do we capture lessons after each planning cycle?",
                ReflectionQuestion = "Can a team member point to the plan that guides their week?"
            },
            new Item
            {
                Id = 11,
                Code = "4.1",
                Name = "Organisational capability through people",
                ShortDescription = "People capability is built to meet the organisation’s goals.",
                CategoryId = 4,
                WhyItMatters = "Strategy only works if people have the skills to deliver it. Building capability on purpose reduces reliance on a few key individuals.",
                ApproachPoints = "How do we identify the capabilities we need for our goals?|How do we plan to build or hire those capabilities?|How do people plans link to strategy?",
                DeploymentPoints = "Are capability plans applied across all critical roles?|Do teams know the skills they are expected to grow?|Is capability building consistent across locations?",
                ResultsPoints = "What shows we have the people capability we need?|Where do skill gaps still slow delivery?|What evidence links people investment to performance?",
                ImprovementPoints = "How do we review workforce capability against strategy?|What do we change when new skills become essential?|How do we learn from roles that are hard to fill or retain?",
                ReflectionQuestion = "Which capability, if missing next year, would put our goals at risk?"
            },
            new Item
            {
                Id = 12,
                Code = "4.2",
                Name = "People effectiveness and development",
                ShortDescription = "People are supported to grow and perform effectively.",
                CategoryId = 4,
                WhyItMatters = "People perform better when expectations are clear and development is real. Growth support also helps retain the talent you need.",
                ApproachPoints = "How do we set expectations for effective performance?|What development pathways do we offer?|How do managers support learning on the job?",
                DeploymentPoints = "Are development practices available to all employees?|Do managers apply coaching and feedback consistently?|Is development linked to real work, not only courses?",
                ResultsPoints = "What shows people are becoming more effective?|How do we track development progress and outcomes?|What feedback tells us support is working?",
                ImprovementPoints = "How do we improve development offerings over time?|When do we adjust approaches that are not working?|How do we share effective coaching practices?",
                ReflectionQuestion = "Would most people say they are growing here, or just getting through the week?"
            },
            new Item
            {
                Id = 13,
                Code = "4.3",
                Name = "A great place to work",
                ShortDescription = "The workplace supports wellbeing, engagement and pride in work.",
                CategoryId = 4,
                WhyItMatters = "A workplace people value attracts and keeps talent. Wellbeing and pride also lift day-to-day quality and collaboration.",
                ApproachPoints = "How do we define what a great place to work means here?|What practices support wellbeing and engagement?|How do leaders plan to build pride in the work?",
                DeploymentPoints = "Are wellbeing and engagement practices felt in every team?|Do remote and on-site staff get equitable support?|Is recognition used fairly across the organisation?",
                ResultsPoints = "What engagement or wellbeing measures do we track?|How do retention and referral trends look?|What stories show people feel proud to work here?",
                ImprovementPoints = "How do we act on engagement survey findings?|What workplace issues do we fix first?|How do we keep improving the employee experience?",
                ReflectionQuestion = "Would you recommend this workplace to someone you care about?"
            },
            new Item
            {
                Id = 14,
                Code = "5.1",
                Name = "Identifying, generating, collecting and analysing the right data to inform decision-making",
                ShortDescription = "The right data is gathered and analysed to guide decisions.",
                CategoryId = 5,
                WhyItMatters = "Decisions are only as good as the information behind them. Focusing on the right data saves time and reduces guesswork.",
                ApproachPoints = "How do we decide which data matters for decisions?|How do we collect and quality-check that data?|How is analysis designed to support leaders?",
                DeploymentPoints = "Do decision-makers get timely access to the right data?|Is data practice consistent across teams?|Are definitions shared so measures mean the same thing?",
                ResultsPoints = "What shows decisions are better because of our data?|How often is key data late, incomplete or unused?|What evidence shows analysis quality is improving?",
                ImprovementPoints = "How do we review whether we still measure the right things?|What data do we stop collecting because it adds noise?|How do we improve analysis skills across the organisation?",
                ReflectionQuestion = "Which report would we miss least if it disappeared tomorrow?"
            },
            new Item
            {
                Id = 15,
                Code = "5.2",
                Name = "Creating value through applying knowledge",
                ShortDescription = "Knowledge is used in ways that create real value.",
                CategoryId = 5,
                WhyItMatters = "Stored knowledge has little worth until people apply it. Sharing and using know-how helps teams avoid repeating mistakes.",
                ApproachPoints = "How do we capture knowledge that creates value?|How do people find and apply what others have learned?|How do we encourage knowledge sharing as normal work?",
                DeploymentPoints = "Is useful knowledge available across teams and sites?|Do people reuse lessons before starting new work?|Are knowledge tools part of everyday processes?",
                ResultsPoints = "What shows knowledge reuse is creating value?|How do we know critical know-how is not locked in a few heads?|What examples prove shared learning improved outcomes?",
                ImprovementPoints = "How do we improve knowledge capture and access?|What barriers to sharing do we remove?|How do we refresh knowledge that has become outdated?",
                ReflectionQuestion = "Where does important know-how still live only in one person’s notebook or head?"
            },
            new Item
            {
                Id = 16,
                Code = "5.3",
                Name = "Enable innovation and the use of technology",
                ShortDescription = "Technology and new ideas are used to improve how work is done.",
                CategoryId = 5,
                WhyItMatters = "Technology and innovation help the organisation stay relevant. Used well, they free people for higher-value work and better services.",
                ApproachPoints = "How do we decide where innovation and technology will help most?|What process turns ideas into tested changes?|How do we choose and adopt technology thoughtfully?",
                DeploymentPoints = "Are innovation practices open to people beyond one team?|Is useful technology rolled out consistently?|Do teams get support to adopt new tools well?",
                ResultsPoints = "What shows innovation is delivering real benefits?|How do we measure technology adoption and value?|What evidence shows ideas move from concept to use?",
                ImprovementPoints = "How do we review failed or stalled innovations?|What do we change in our technology choices over time?|How do we keep the innovation pipeline healthy?",
                ReflectionQuestion = "Are we adopting technology because it helps people, or because it is new?"
            },
            new Item
            {
                Id = 17,
                Code = "6.1",
                Name = "Identification and management of organisational processes",
                ShortDescription = "Key processes are identified, owned and managed clearly.",
                CategoryId = 6,
                WhyItMatters = "Clear process ownership reduces waste and confusion. When key work is managed well, quality and speed become more predictable.",
                ApproachPoints = "How do we identify the processes that matter most?|Who owns each key process and what are they accountable for?|How are process standards and measures defined?",
                DeploymentPoints = "Are key processes documented and used in daily work?|Is ownership clear across all units involved?|Do handoffs between teams follow agreed steps?",
                ResultsPoints = "What process measures show stability or problems?|How often do process failures cause customer issues?|What evidence shows process ownership is working?",
                ImprovementPoints = "How do we review process performance and ownership?|When do we redesign a process that no longer fits?|How do we learn from process breakdowns?",
                ReflectionQuestion = "If a key process failed tomorrow, who would own fixing it?"
            },
            new Item
            {
                Id = 18,
                Code = "6.2",
                Name = "Management of supplier and stakeholder processes",
                ShortDescription = "Processes with suppliers and stakeholders are managed effectively.",
                CategoryId = 6,
                WhyItMatters = "Much of your performance depends on partners outside your walls. Managing those shared processes well protects quality and relationships.",
                ApproachPoints = "How do we define processes that involve suppliers and stakeholders?|What expectations and standards guide those partnerships?|How do we manage risk in shared processes?",
                DeploymentPoints = "Are supplier and stakeholder processes applied consistently?|Do partners understand their role in the process?|Is performance reviewed with suppliers, not only internally?",
                ResultsPoints = "What shows supplier and partner processes deliver as expected?|How do we track quality, timeliness or issues from partners?|What feedback do stakeholders give on working with us?",
                ImprovementPoints = "How do we improve shared processes with partners?|When do we change a supplier arrangement that is not working?|How do we share learning across partner relationships?",
                ReflectionQuestion = "Which supplier or partner process would cause the biggest problem if it slipped?"
            },
            new Item
            {
                Id = 19,
                Code = "6.3",
                Name = "Process improvement and innovation",
                ShortDescription = "Processes are improved and innovated over time.",
                CategoryId = 6,
                WhyItMatters = "Even good processes drift or become outdated. Regular improvement and occasional reinvention keep work efficient and relevant.",
                ApproachPoints = "How do we decide which processes to improve or reinvent?|What methods guide improvement and innovation work?|How do people raise and test process ideas?",
                DeploymentPoints = "Is improvement activity happening across the organisation?|Do teams have time and tools to improve their processes?|Are successful changes spread beyond the first team?",
                ResultsPoints = "What gains have process improvements delivered?|How many improvements move from idea to lasting change?|What evidence shows innovation in processes, not only small fixes?",
                ImprovementPoints = "How do we review our improvement approach itself?|What blocks people from improving processes?|How do we keep momentum when early wins fade?",
                ReflectionQuestion = "When did we last make a process simpler rather than add another step?"
            },
            new Item
            {
                Id = 20,
                Code = "7.1",
                Name = "Measuring and communicating organisational results",
                ShortDescription = "Results are measured and shared so people know how the organisation is performing.",
                CategoryId = 7,
                WhyItMatters = "People cannot improve what they cannot see. Clear measures and honest communication keep everyone aligned on performance.",
                ApproachPoints = "How do we choose the results that matter most?|How are results measured and validated?|How do we plan to communicate results to different audiences?",
                DeploymentPoints = "Are key results visible to the people who can act on them?|Is communication timely and consistent across units?|Do leaders discuss results with their teams regularly?",
                ResultsPoints = "What shows our measures give a true performance picture?|How well do people understand current results?|What evidence links result communication to better action?",
                ImprovementPoints = "How do we improve the measures we use over time?|When do we change how results are shared?|How do we learn from results that surprised us?",
                ReflectionQuestion = "Could most people explain our three most important results without looking them up?"
            },
            new Item
            {
                Id = 21,
                Code = "7.2",
                Name = "Achieving sustainable performance",
                ShortDescription = "Performance is sustained for the long term, not only short-term gains.",
                CategoryId = 7,
                WhyItMatters = "Short-term wins mean little if they cannot be sustained. Lasting performance balances today’s results with tomorrow’s capacity.",
                ApproachPoints = "How do we define sustainable performance for our organisation?|What long-term indicators sit alongside short-term targets?|How do we plan to protect capability while delivering results?",
                DeploymentPoints = "Are sustainable-performance practices used across the organisation?|Do teams balance delivery pressure with long-term health?|Are long-term indicators reviewed as often as short-term ones?",
                ResultsPoints = "What trends show performance is holding over time?|Where have short-term gains created later problems?|What evidence shows we can sustain results under pressure?",
                ImprovementPoints = "How do we review whether performance is truly sustainable?|What do we change when gains start to fade?|How do we learn from periods of strong and weak sustainability?",
                ReflectionQuestion = "Are today’s results building the organisation we will need in three years?"
            });

        builder.Entity<Principle>().HasData(
            new Principle { Id = 1, Number = 1, Name = "Clear Direction and Organisational Alignment", ShortDescription = "Shared direction helps everyone pull toward the same goals." },
            new Principle { Id = 2, Number = 2, Name = "Ethical Governance", ShortDescription = "Honest, responsible governance builds trust and good decisions." },
            new Principle { Id = 3, Number = 3, Name = "Customer and Stakeholder Value", ShortDescription = "Excellence starts with creating value for customers and stakeholders." },
            new Principle { Id = 4, Number = 4, Name = "A Systems Perspective", ShortDescription = "The organisation works best when its parts are managed as one system." },
            new Principle { Id = 5, Number = 5, Name = "Valuing People", ShortDescription = "People’s skill, energy and involvement lift organisational performance." },
            new Principle { Id = 6, Number = 6, Name = "Organisational Learning", ShortDescription = "Learning and improvement keep the organisation adaptable." },
            new Principle { Id = 7, Number = 7, Name = "Management by Fact", ShortDescription = "Good decisions are based on reliable facts and evidence." },
            new Principle { Id = 8, Number = 8, Name = "Societal Contributions", ShortDescription = "Organisations create value for the wider community, not only themselves." },
            new Principle { Id = 9, Number = 9, Name = "Visionary Leadership", ShortDescription = "Leaders set the tone and inspire a future-focused culture." });

        builder.Entity<Event>().HasData(
            new Event
            {
                Id = 1,
                Title = "ABEF Introduction Webinar",
                Date = new DateTime(2026, 12, 4, 10, 0, 0),
                Location = "Online",
                Type = "Webinar",
                ShortDescription = "Sample event: a short online introduction to the Australian Business Excellence Framework.",
                RegisterUrl = "#",
                IsPublished = true
            },
            new Event
            {
                Id = 2,
                Title = "ADRI Workshop",
                Date = new DateTime(2027, 1, 15, 9, 30, 0),
                Location = "Sydney",
                Type = "Workshop",
                ShortDescription = "Sample event: a hands-on workshop on using ADRI for reflection and improvement.",
                RegisterUrl = "#",
                IsPublished = true
            },
            new Event
            {
                Id = 3,
                Title = "Self-Assessment Masterclass",
                Date = new DateTime(2027, 2, 12, 13, 0, 0),
                Location = "Melbourne",
                Type = "Workshop",
                ShortDescription = "Sample event: learn how to run a practical organisational self-assessment.",
                RegisterUrl = "#",
                IsPublished = true
            },
            new Event
            {
                Id = 4,
                Title = "Business Excellence Forum",
                Date = new DateTime(2027, 3, 20, 9, 0, 0),
                Location = "Brisbane",
                Type = "Conference",
                ShortDescription = "Sample event: a full-day forum sharing practice, results and lessons from excellence work.",
                RegisterUrl = "#",
                IsPublished = true
            });
    }
}

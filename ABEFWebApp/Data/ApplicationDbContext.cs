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
                ShortDescription = "Sets direction, culture and good governance."
            },
            new Category
            {
                Id = 2,
                Number = 2,
                Name = "Customers and other Stakeholders",
                Type = "Driver",
                ShortDescription = "Understands and delivers value to customers and other stakeholders."
            },
            new Category
            {
                Id = 3,
                Number = 3,
                Name = "Strategy and Planning",
                Type = "Enabler",
                ShortDescription = "Turns direction into clear plans and goals."
            },
            new Category
            {
                Id = 4,
                Number = 4,
                Name = "People",
                Type = "Enabler",
                ShortDescription = "Develops, supports and engages the workforce."
            },
            new Category
            {
                Id = 5,
                Number = 5,
                Name = "Information and Knowledge",
                Type = "Enabler",
                ShortDescription = "Uses data and knowledge to make better decisions."
            },
            new Category
            {
                Id = 6,
                Number = 6,
                Name = "Process Management, Improvement and Innovation",
                Type = "Enabler",
                ShortDescription = "Designs, improves and innovates how work gets done."
            },
            new Category
            {
                Id = 7,
                Number = 7,
                Name = "Results and Sustainable Performance",
                Type = "Result",
                ShortDescription = "Measures outcomes and long-term performance."
            });

        builder.Entity<Item>().HasData(
            new Item { Id = 1, Code = "1.1", Name = "Leadership and governance throughout the organisation", ShortDescription = "Leaders set clear expectations and govern the whole organisation well.", CategoryId = 1 },
            new Item { Id = 2, Code = "1.2", Name = "Leading the organisational culture", ShortDescription = "Leaders shape a culture that supports excellence every day.", CategoryId = 1 },
            new Item { Id = 3, Code = "1.3", Name = "Purpose and Vision", ShortDescription = "The organisation is guided by a clear purpose and vision.", CategoryId = 1 },
            new Item { Id = 4, Code = "1.4", Name = "Society, community and environmental responsibility", ShortDescription = "The organisation acts responsibly toward society, community and the environment.", CategoryId = 1 },

            new Item { Id = 5, Code = "2.1", Name = "Knowledge of markets, customers and other stakeholders", ShortDescription = "The organisation understands its markets, customers and other stakeholders.", CategoryId = 2 },
            new Item { Id = 6, Code = "2.2", Name = "Effective management of customer and other stakeholder relationships", ShortDescription = "Relationships with customers and stakeholders are managed carefully and well.", CategoryId = 2 },
            new Item { Id = 7, Code = "2.3", Name = "Measuring customers and other stakeholders' perception of value", ShortDescription = "The organisation checks how customers and stakeholders judge the value they receive.", CategoryId = 2 },

            new Item { Id = 8, Code = "3.1", Name = "Understand the ecosystem and organisational capability", ShortDescription = "Leaders understand the wider environment and what the organisation can do.", CategoryId = 3 },
            new Item { Id = 9, Code = "3.2", Name = "Understand stakeholder requirements", ShortDescription = "Plans are shaped by what stakeholders need and expect.", CategoryId = 3 },
            new Item { Id = 10, Code = "3.3", Name = "The planning process", ShortDescription = "Planning turns direction into practical goals and actions.", CategoryId = 3 },

            new Item { Id = 11, Code = "4.1", Name = "Organisational capability through people", ShortDescription = "People capability is built to meet the organisation’s goals.", CategoryId = 4 },
            new Item { Id = 12, Code = "4.2", Name = "People effectiveness and development", ShortDescription = "People are supported to grow and perform effectively.", CategoryId = 4 },
            new Item { Id = 13, Code = "4.3", Name = "A great place to work", ShortDescription = "The workplace supports wellbeing, engagement and pride in work.", CategoryId = 4 },

            new Item { Id = 14, Code = "5.1", Name = "Identifying, generating, collecting and analysing the right data to inform decision-making", ShortDescription = "The right data is gathered and analysed to guide decisions.", CategoryId = 5 },
            new Item { Id = 15, Code = "5.2", Name = "Creating value through applying knowledge", ShortDescription = "Knowledge is used in ways that create real value.", CategoryId = 5 },
            new Item { Id = 16, Code = "5.3", Name = "Enable innovation and the use of technology", ShortDescription = "Technology and new ideas are used to improve how work is done.", CategoryId = 5 },

            new Item { Id = 17, Code = "6.1", Name = "Identification and management of organisational processes", ShortDescription = "Key processes are identified, owned and managed clearly.", CategoryId = 6 },
            new Item { Id = 18, Code = "6.2", Name = "Management of supplier and stakeholder processes", ShortDescription = "Processes with suppliers and stakeholders are managed effectively.", CategoryId = 6 },
            new Item { Id = 19, Code = "6.3", Name = "Process improvement and innovation", ShortDescription = "Processes are improved and innovated over time.", CategoryId = 6 },

            new Item { Id = 20, Code = "7.1", Name = "Measuring and communicating organisational results", ShortDescription = "Results are measured and shared so people know how the organisation is performing.", CategoryId = 7 },
            new Item { Id = 21, Code = "7.2", Name = "Achieving sustainable performance", ShortDescription = "Performance is sustained for the long term, not only short-term gains.", CategoryId = 7 });

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

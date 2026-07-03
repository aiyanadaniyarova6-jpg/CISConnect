using CISConnect.Models;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CISConnect.Data;

// Main database context — extends Identity so admin users share the same DB.
public class ApplicationDbContext : IdentityDbContext<IdentityUser>, IDataProtectionKeyContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<DataProtectionKey> DataProtectionKeys => Set<DataProtectionKey>();

    public DbSet<UpdatePost> UpdatePosts => Set<UpdatePost>();
    public DbSet<MenuSection> MenuSections => Set<MenuSection>();
    public DbSet<GuideArticle> GuideArticles => Set<GuideArticle>();
    public DbSet<University> Universities => Set<University>();
    public DbSet<UniversitySection> UniversitySections => Set<UniversitySection>();
    public DbSet<FAQItem> FAQItems => Set<FAQItem>();
    public DbSet<ContactItem> ContactItems => Set<ContactItem>();

    // Column length limits for MySQL — prevents "row too large" errors on older engines.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UpdatePost>(entity =>
        {
            entity.Property(post => post.Title).HasMaxLength(200).IsRequired();
            entity.Property(post => post.Summary).HasMaxLength(500);
            entity.Property(post => post.Category).HasMaxLength(100).IsRequired();
            entity.Property(post => post.MediaType).HasMaxLength(20);
            entity.Property(post => post.MediaUrl).HasMaxLength(1000);
            entity.Property(post => post.MediaAltText).HasMaxLength(250);
            entity.Property(post => post.SourceName).HasMaxLength(200);
            entity.Property(post => post.SourceUrl).HasMaxLength(1000);
        });

        modelBuilder.Entity<MenuSection>(entity =>
        {
            entity.Property(section => section.Name).HasMaxLength(100).IsRequired();
            entity.Property(section => section.Slug).HasMaxLength(100).IsRequired();
            entity.Property(section => section.Description).HasMaxLength(500);
        });

        modelBuilder.Entity<GuideArticle>(entity =>
        {
            entity.Property(article => article.Title).HasMaxLength(200).IsRequired();
            entity.Property(article => article.Summary).HasMaxLength(500);
            entity.Property(article => article.SourceName).HasMaxLength(200);
            entity.Property(article => article.SourceUrl).HasMaxLength(1000);
            entity.Property(article => article.UniversityTag).HasMaxLength(100);

            entity.HasOne(article => article.MenuSection)
                .WithMany(section => section.GuideArticles)
                .HasForeignKey(article => article.MenuSectionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<University>(entity =>
        {
            entity.Property(university => university.Name).HasMaxLength(150).IsRequired();
            entity.Property(university => university.Location).HasMaxLength(150);
            entity.Property(university => university.CampusMapLink).HasMaxLength(500);
        });

        modelBuilder.Entity<UniversitySection>(entity =>
        {
            entity.Property(section => section.SectionType).HasMaxLength(100).IsRequired();
            entity.Property(section => section.Title).HasMaxLength(200).IsRequired();
            entity.Property(section => section.SourceName).HasMaxLength(200);
            entity.Property(section => section.SourceUrl).HasMaxLength(1000);

            entity.HasOne(section => section.University)
                .WithMany(university => university.Sections)
                .HasForeignKey(section => section.UniversityId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<FAQItem>(entity =>
        {
            entity.Property(item => item.Category).HasMaxLength(100).IsRequired();
            entity.Property(item => item.Question).HasMaxLength(250).IsRequired();
        });

        modelBuilder.Entity<ContactItem>(entity =>
        {
            entity.Property(contact => contact.Category).HasMaxLength(100).IsRequired();
            entity.Property(contact => contact.Name).HasMaxLength(150).IsRequired();
            entity.Property(contact => contact.Phone).HasMaxLength(50);
            entity.Property(contact => contact.Email).HasMaxLength(150);
        });
    }
}

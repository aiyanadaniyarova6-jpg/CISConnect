namespace CISConnect.Models;

public class GuideArticle
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string SourceName { get; set; } = string.Empty;
    public string SourceUrl { get; set; } = string.Empty;
    public DateTime? LastVerifiedAt { get; set; }

    public string? UniversityTag { get; set; }
    public string? CountryTag { get; set; }

    public int MenuSectionId { get; set; }
    public MenuSection? MenuSection { get; set; }
}

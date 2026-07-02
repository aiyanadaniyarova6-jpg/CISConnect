namespace CISConnect.Models;

public class UniversitySection
{
    public int Id { get; set; }
    public string SectionType { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string SourceName { get; set; } = string.Empty;
    public string SourceUrl { get; set; } = string.Empty;
    public DateTime? LastVerifiedAt { get; set; }

    public int UniversityId { get; set; }
    public University? University { get; set; }
}

namespace CISConnect.ViewModels;

public class PostCardViewModel
{
    public string PublicId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public DateTime PublishedAt { get; set; }
    public string MediaType { get; set; } = string.Empty;
    public string MediaUrl { get; set; } = string.Empty;
    public string MediaAltText { get; set; } = string.Empty;
    public bool IsHighlighted { get; set; }
    public bool ShowFullContent { get; set; }
}

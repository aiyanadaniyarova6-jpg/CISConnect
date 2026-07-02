namespace CISConnect.ViewModels;

public class PostDetailsViewModel
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
    public string SourceName { get; set; } = string.Empty;
    public string SourceUrl { get; set; } = string.Empty;
    public DateTime? LastVerifiedAt { get; set; }
    public bool IsHighlighted { get; set; }
    public string SourceLabel { get; set; } = string.Empty;
    public string BackController { get; set; } = string.Empty;
    public string BackAction { get; set; } = string.Empty;
    public string BackButtonText { get; set; } = "Back to Home";
    public IEnumerable<BreadcrumbItemViewModel> Breadcrumbs { get; set; } = Enumerable.Empty<BreadcrumbItemViewModel>();
    public PostSummaryViewModel? PreviousPost { get; set; }
    public PostSummaryViewModel? NextPost { get; set; }
    public IEnumerable<PostSummaryViewModel> RelatedPosts { get; set; } = Enumerable.Empty<PostSummaryViewModel>();
}

public class BreadcrumbItemViewModel
{
    public string Label { get; set; } = string.Empty;
    public string Controller { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public bool IsCurrent { get; set; }
}

public class PostSummaryViewModel
{
    public string PublicId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public DateTime PublishedAt { get; set; }
    public string SourceLabel { get; set; } = string.Empty;
}

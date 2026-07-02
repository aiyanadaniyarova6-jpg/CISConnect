using System.ComponentModel.DataAnnotations;

namespace CISConnect.Models;

public class UpdatePost
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    [StringLength(200, ErrorMessage = "Title must be 200 characters or fewer.")]
    public string Title { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Summary must be 500 characters or fewer.")]
    public string Summary { get; set; } = string.Empty;

    [Required(ErrorMessage = "Content is required.")]
    public string Content { get; set; } = string.Empty;

    [Required(ErrorMessage = "Category is required.")]
    [StringLength(100, ErrorMessage = "Category must be 100 characters or fewer.")]
    public string Category { get; set; } = string.Empty;

    [StringLength(20, ErrorMessage = "Media type must be 20 characters or fewer.")]
    public string MediaType { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "Media URL must be 1000 characters or fewer.")]
    public string MediaUrl { get; set; } = string.Empty;

    [StringLength(250, ErrorMessage = "Alt text must be 250 characters or fewer.")]
    public string MediaAltText { get; set; } = string.Empty;

    [StringLength(200, ErrorMessage = "Source name must be 200 characters or fewer.")]
    public string SourceName { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "Source URL must be 1000 characters or fewer.")]
    public string SourceUrl { get; set; } = string.Empty;

    public DateTime? LastVerifiedAt { get; set; }
    public DateTime PublishedAt { get; set; }
    public bool IsHighlighted { get; set; }
}

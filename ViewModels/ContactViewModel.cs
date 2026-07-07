using System.ComponentModel.DataAnnotations;

namespace CISConnect.ViewModels;

public class ContactViewModel
{
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Enter a valid email")]
    [MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Subject is required")]
    [MaxLength(200)]
    public string Subject { get; set; } = string.Empty;

    [Required(ErrorMessage = "Message is required")]
    [MaxLength(2000)]
    public string Message { get; set; } = string.Empty;

    public bool Sent { get; set; }
    public bool Error { get; set; }

    public string? ReturnUrl { get; set; }
}

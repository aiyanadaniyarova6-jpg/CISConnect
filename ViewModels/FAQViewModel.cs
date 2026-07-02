using CISConnect.Models;

namespace CISConnect.ViewModels;

public class FAQViewModel
{
    public string SearchTerm { get; set; } = string.Empty;
    public IEnumerable<FAQItem> Items { get; set; } = Enumerable.Empty<FAQItem>();
}

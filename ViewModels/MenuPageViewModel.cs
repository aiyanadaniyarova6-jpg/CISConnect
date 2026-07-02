using CISConnect.Models;

namespace CISConnect.ViewModels;

public class MenuPageViewModel
{
    public MenuSection? Section { get; set; }
    public IEnumerable<GuideArticle> Articles { get; set; } = Enumerable.Empty<GuideArticle>();
    public IEnumerable<ContactItem> Contacts { get; set; } = Enumerable.Empty<ContactItem>();
    public IEnumerable<CountrySupportViewModel> CountrySupportItems { get; set; } = Enumerable.Empty<CountrySupportViewModel>();
}

public class CountrySupportViewModel
{
    public string Country { get; set; } = string.Empty;
    public string EmbassyName { get; set; } = string.Empty;
    public string EmbassyUrl { get; set; } = string.Empty;
    public string CommunityNote { get; set; } = string.Empty;
    public string StudentTip { get; set; } = string.Empty;
}

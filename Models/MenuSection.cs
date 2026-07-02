namespace CISConnect.Models;

public class MenuSection
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public ICollection<GuideArticle> GuideArticles { get; set; } = new List<GuideArticle>();
}

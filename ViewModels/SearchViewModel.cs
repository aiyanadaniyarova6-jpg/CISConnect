namespace CISConnect.ViewModels;

public class SearchViewModel
{
    public string Query { get; set; } = string.Empty;
    public IEnumerable<PostSummaryViewModel> Results { get; set; } = Enumerable.Empty<PostSummaryViewModel>();
    public IEnumerable<UniversitySearchResult> UniversityResults { get; set; } = Enumerable.Empty<UniversitySearchResult>();
    public int TotalCount => Results.Count() + UniversityResults.Count();
}

public record UniversitySearchResult(int Id, string Name, string Location, string Description);

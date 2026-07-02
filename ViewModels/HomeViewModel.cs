using CISConnect.Models;

namespace CISConnect.ViewModels;

public class HomeViewModel
{
    public IEnumerable<UpdatePost> HighlightedUpdates { get; set; } = Enumerable.Empty<UpdatePost>();
    public IEnumerable<UpdatePost> LatestUpdates { get; set; } = Enumerable.Empty<UpdatePost>();
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; } = 1;
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
}

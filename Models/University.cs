namespace CISConnect.Models;

public class University
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CampusMapLink { get; set; } = string.Empty;

    public ICollection<UniversitySection> Sections { get; set; } = new List<UniversitySection>();
}

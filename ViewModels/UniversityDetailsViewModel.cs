using CISConnect.Models;

namespace CISConnect.ViewModels;

public class UniversityDetailsViewModel
{
    public static readonly string[] SectionOrder =
    [
        "Overview",
        "Campus Map",
        "Programs",
        "Requirements",
        "Deadlines",
        "Fees & Prices",
        "Scholarships",
        "Housing",
        "Useful Contacts",
        "Cafeteria / Food",
        "Facilities",
        "Local Updates"
    ];

    public University? University { get; set; }
    public IEnumerable<UniversitySection> Sections { get; set; } = Enumerable.Empty<UniversitySection>();

    public UniversitySection? GetSection(string sectionType)
    {
        return Sections.FirstOrDefault(section => section.SectionType == sectionType);
    }

    public IEnumerable<UniversitySection> OrderedSections()
    {
        var ordered = SectionOrder
            .Select(GetSection)
            .Where(section => section is not null)
            .Cast<UniversitySection>();

        var otherSections = Sections
            .Where(section => !SectionOrder.Contains(section.SectionType))
            .OrderBy(section => section.SectionType);

        return ordered.Concat(otherSections);
    }
}

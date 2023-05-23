using Common.Domain;

namespace CoreModule.Domain.Course.Entities;

public class Section:BaseEntity
{
    public Section(string title, int displayOrder)
    {
        Title = title;
        DisplayOrder = displayOrder;
    }

    public string Title { get;private set; }
    public int DisplayOrder { get;private set; } 

    public IEnumerable<Episode> Episodes { get; set; }
}

using Common.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreModule.Query._Data.Entities;

[Table("Sections",Schema ="course")]
class SectionQueryModel : BaseEntity
{
    public Guid CourseId { get; set; }
    public string Title { get; set; }
    public int DisplayOrder { get; set; }

    public IEnumerable<EpisodeQueryModel> Episodes { get;   set; }

    [ForeignKey("CourseId")]
    public CourseQueryModel Course { get; set; }
}

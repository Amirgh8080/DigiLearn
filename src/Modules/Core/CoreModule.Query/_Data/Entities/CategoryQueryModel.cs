using Common.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreModule.Query._Data.Entities;

[Table("Categories",Schema ="dbo")]
class CategoryQueryModel : BaseEntity
{
    public string Title { get; private set; }
    public string Slug { get; private set; }
    public Guid? ParentId { get; private set; }

    [ForeignKey("ParentId")]
    public List<CategoryQueryModel> Children { get; set; }
}

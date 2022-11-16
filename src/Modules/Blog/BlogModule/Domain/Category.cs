using Common.Domain;
using System.ComponentModel.DataAnnotations;

namespace BlogModule.Domain;

class Category:BaseEntity
{
    [MaxLength(80)]
    public string Title { get; set; }
    
    [MaxLength(80)]
    public string Slug { get; set; }
}

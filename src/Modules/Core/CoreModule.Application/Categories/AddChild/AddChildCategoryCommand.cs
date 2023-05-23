using Common.Application;

namespace CoreModule.Application.Categories.AddChild;

public class AddChildCategoryCommand:IBaseCommand
{
    public Guid ParentId { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
}

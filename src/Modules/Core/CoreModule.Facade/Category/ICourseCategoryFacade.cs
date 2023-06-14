using Common.Application;
using CoreModule.Application.Categories.AddChild;
using CoreModule.Application.Categories.Create;
using CoreModule.Application.Categories.Delete;
using CoreModule.Application.Categories.Edit;
using CoreModule.Query.Category._DTOs;
using CoreModule.Query.Category.GetAll;
using CoreModule.Query.Category.GetChildren;
using MediatR;

namespace CoreModule.Facade.Category;

public interface ICourseCategoryFacade
{
    Task<OperationResult> Create(CreateCategoryCommand command);
    Task<OperationResult> Delete(DeleteCategoryCommand command);
    Task<OperationResult> Edit(EditCategoryCommand command);
    Task<OperationResult> AddChild(AddChildCategoryCommand command);

    Task<List<CourseCategoryDto>> GetMainCategories();
    Task<List<CourseCategoryDto>> GetChildern(Guid parentId);
}
class CourseCategoryFacade : ICourseCategoryFacade
{
    private readonly IMediator _mediator;

    public CourseCategoryFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> AddChild(AddChildCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Create(CreateCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public  async Task<OperationResult> Delete(DeleteCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public  async Task<OperationResult> Edit(EditCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<List<CourseCategoryDto>> GetChildern(Guid parentId)
    {
        return await _mediator.Send(new GetCourseCategoryChildrenQuery(parentId));
    }

    public async Task<List<CourseCategoryDto>> GetMainCategories()
    {
        return await _mediator.Send(new GetAllCourseCategoriesQuery());
    }
}

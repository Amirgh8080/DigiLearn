using AutoMapper;
using BlogModule.Domain;
using BlogModule.Repositories.Categories;
using BlogModule.Repositories.Posts;
using BlogModule.Services.DTOs.Command;
using BlogModule.Services.DTOs.Query;
using Common.Application;

namespace BlogModule.Services;

public interface IBlogService
{
    Task<OperationResult> CreateCategory(CreateCategoryCommand command);
    Task<OperationResult> EditCategory(EditCategoryCommand command);
    Task<OperationResult> DeleteCategory(Guid categoryId);
    Task<List<BlogCategoryDto>> GetAllCategories();
    Task<BlogCategoryDto> GetCategoryById(Guid categoryId);
}

class BlogService : IBlogService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly IPostRepository _postRepository;
    public BlogService(ICategoryRepository repository, IMapper mapper, IPostRepository postRepository)
    {
        _categoryRepository = repository;
        _mapper = mapper;
        _postRepository = postRepository;
    }

    public async Task<OperationResult> CreateCategory(CreateCategoryCommand command)
    {
        var category = _mapper.Map<Category>(command);

        if (await _categoryRepository.ExistsAsync(c => c.Slug == category.Slug))
        {
            return OperationResult.Error("Slug is Exist");
        }

        await _categoryRepository.AddAsync(category);
        await _categoryRepository.Save();

        return OperationResult.Success();
    }

    public async Task<OperationResult> DeleteCategory(Guid categoryId)
    {
        var category = await _categoryRepository.GetAsync(categoryId);

        if (category == null)
            return OperationResult.NotFound();

        if (await _postRepository.ExistsAsync(c => c.CategoryId == categoryId))
            return OperationResult.Error("این دسته بندی قبلا استفاده شده است . لطفا پست های مربوطه را پاک کنید و دوباره امتحان کنید");

        _categoryRepository.Delete(category);
        await _categoryRepository.Save();

        return OperationResult.Success();
    }

    public async Task<OperationResult> EditCategory(EditCategoryCommand command)
    {
        var category = await _categoryRepository.GetAsync(command.Id);

        if (category == null)
            return OperationResult.NotFound();

        if (category.Slug != command.Slug)
        {

            if (await _categoryRepository.ExistsAsync(c => c.Slug == category.Slug))
            {
                return OperationResult.Error("Slug is Exist");
            }
        }

        category.Slug = command.Slug;
        category.Title = command.Title;

        _categoryRepository.Update(category);
        await _categoryRepository.Save();

        return OperationResult.Success();
    }

    public async Task<List<BlogCategoryDto>> GetAllCategories()
    {
        var categories = await _categoryRepository.GetAll();
        return _mapper.Map<List<BlogCategoryDto>>(categories);
    }

    public async Task<BlogCategoryDto> GetCategoryById(Guid categoryId)
    {
        var category = await _categoryRepository.GetAsync(categoryId);

        return _mapper.Map<BlogCategoryDto>(category);

    }
}

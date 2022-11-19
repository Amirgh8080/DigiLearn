using AutoMapper;
using BlogModule.Domain;
using BlogModule.Repositories.Categories;
using BlogModule.Repositories.Posts;
using BlogModule.Services.DTOs.Command;
using BlogModule.Services.DTOs.Query;
using BlogModule.Utils;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Application.SecurityUtil;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace BlogModule.Services;

public interface IBlogService
{
    #region Category Services

    Task<OperationResult> CreateCategory(CreateCategoryCommand command);
    Task<OperationResult> EditCategory(EditCategoryCommand command);
    Task<OperationResult> DeleteCategory(Guid categoryId);
    Task<List<BlogCategoryDto>> GetAllCategories();
    Task<BlogCategoryDto?> GetCategoryById(Guid categoryId);

    #endregion

    #region Post Services

    Task<OperationResult> CreatePost(CreatePostCommand command);
    Task<OperationResult> EditPost(EditPostCommand command);
    Task<OperationResult> DeletePost(Guid postId);

    Task<BlogPostDto?> GetBlogPostById(Guid postId);


    #endregion
}

class BlogService : IBlogService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly IPostRepository _postRepository;
    private readonly ILocalFileService _localFileService;
    public BlogService(ICategoryRepository repository, IMapper mapper, IPostRepository postRepository, ILocalFileService localFileService)
    {
        _categoryRepository = repository;
        _mapper = mapper;
        _postRepository = postRepository;
        _localFileService = localFileService;
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

    public async Task<OperationResult> CreatePost(CreatePostCommand command)
    {
        var post = _mapper.Map<Post>(command);

        if (await _postRepository.ExistsAsync(p => p.Slug == command.Slug))
            return OperationResult.Error("اسلاگ وجود دارد");

        if (command.ImageFile.IsImage() == false)
            return OperationResult.Error("عکس وارد شده نامعتبر است");

        var imageName = await _localFileService.SaveFileAndGenerateName(command.ImageFile, BlogDirectories.PostImage);

        post.ImageName = imageName;
        post.Visit = 1;
        post.Description = post.Description.SanitizeText();


        _postRepository.Add(post);
        await _postRepository.Save();

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

    public async Task<OperationResult> DeletePost(Guid postId)
    {
        var post = await _postRepository.GetAsync(postId);

        if (post == null) return OperationResult.NotFound();

         _postRepository.Delete(post);
        await _postRepository.Save();
        _localFileService.DeleteFile(BlogDirectories.PostImage, post.ImageName);
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

    public async Task<OperationResult> EditPost(EditPostCommand command)
    {
        var post = await _postRepository.GetTracking(command.Id);

        if (post == null) return OperationResult.NotFound();

        if (post.Slug != command.Slug)
            if (await _postRepository.ExistsAsync(p => p.Slug == command.Slug))
                return OperationResult.Error("اسلاگ وجود دارد");

        if (command.ImageFile != null)
            if (command.ImageFile.IsImage() == false)
                return OperationResult.Error("عکس وارد شده نامعتبر است");

            else
            {
                var imageName = await _localFileService.SaveFileAndGenerateName(command.ImageFile, BlogDirectories.PostImage);

                post.ImageName = imageName;
            }

        post.OwnerName = command.OwnerName;
        post.Slug = command.Slug;
        post.Description = command.Description.SanitizeText();
        post.Title = command.Title;
        post.CategoryId = command.CategoryId;
        post.UserId = command.UserId; 

        await _postRepository.Save();
        return OperationResult.Success();
    }

    public async Task<List<BlogCategoryDto>> GetAllCategories()
    {
        var categories = await _categoryRepository.GetAll();
        return _mapper.Map<List<BlogCategoryDto>>(categories);
    }

    public async Task<BlogPostDto?> GetBlogPostById(Guid postId)
    {
        var post = await _postRepository.GetAsync(postId);

        if (post == null)
            return null;

        return _mapper.Map<BlogPostDto>(post);

    }

    public async Task<BlogCategoryDto?> GetCategoryById(Guid categoryId)
    {
        var category = await _categoryRepository.GetAsync(categoryId);


        if (category == null)
            return null;

        return _mapper.Map<BlogCategoryDto>(category);

    }
}

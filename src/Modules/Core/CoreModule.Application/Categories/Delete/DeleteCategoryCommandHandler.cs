﻿using Common.Application;
using CoreModule.Domain.Categories.Repository;

namespace CoreModule.Application.Categories.Delete;

public class DeleteCategoryCommandHandler : IBaseCommandHandler<DeleteCategoryCommand>
{
    private readonly ICourseCategoryRepository _categoryRepository;

    public DeleteCategoryCommandHandler(ICourseCategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<OperationResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category =await _categoryRepository.GetTracking(request.CategoryId);
        if (category == null)
            return OperationResult.NotFound();

        await _categoryRepository.Delete(category);

        return OperationResult.Success();
    }
}

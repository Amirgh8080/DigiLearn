using Common.Application.FileUtil.Interfaces;
using CoreModule.Facade.Category;
using Microsoft.AspNetCore.Mvc;

namespace DigiLearn.Web.Controllers;

public class AjaxController : Controller
{
    private readonly ICourseCategoryFacade _categoryFacade;
    private readonly ILocalFileService _localFileService;

    public AjaxController(ICourseCategoryFacade categoryFacade, ILocalFileService localFileService)
    {
        _categoryFacade = categoryFacade;
        _localFileService = localFileService;
    }

    [Route("/ajax/getCategoryChildren")]
    public async Task<IActionResult> GetCategoryChildren(Guid id)
    {
        var text = "";
        var children = await _categoryFacade.GetChildern(id);
        foreach (var item in children)
        {
            text += $"<option value='{item.Id}'>{item.Title}</option>";
        }
        return new ObjectResult(text);
    }

    [Route("/Upload/ImageUploader")]
    public async Task<IActionResult> UploadImage(IFormFile upload)
    {
        if (upload == null)
            return null;

        var fileName = await _localFileService.SaveFileAndGenerateName(upload, "wwwroot/images/upload");

        string url = $"/images/upload/{fileName}";

        return Json(new { Uploaded = true, url });
    }
}

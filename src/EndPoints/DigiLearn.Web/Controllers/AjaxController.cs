using CoreModule.Facade.Category;
using Microsoft.AspNetCore.Mvc;

namespace DigiLearn.Web.Controllers;

public class AjaxController : Controller
{
    private readonly ICourseCategoryFacade _categoryFacade;

    public AjaxController(ICourseCategoryFacade categoryFacade)
    {
        _categoryFacade = categoryFacade;
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
}

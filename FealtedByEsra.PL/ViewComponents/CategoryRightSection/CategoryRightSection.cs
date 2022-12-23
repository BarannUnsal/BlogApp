using FealtedByEsra.BAL.Abstract.Services.CategoryService;
using Microsoft.AspNetCore.Mvc;

namespace FealtedByEsra.PL.ViewComponents.CategoryRightSection
{
    public class CategoryRightSection : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoryRightSection(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _categoryService.GetAll(false);
            return View(categories);
        }
    }
}

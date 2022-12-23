using FealtedByEsra.BAL.Abstract.Services.CategoryService;
using FealtedByEsra.BAL.Filters;
using FealtedByEsra.BAL.Validations.CategoryValidate;
using FealtedByEsra.Entity.Entities;
using FealtedByEsra.PL.Models.Category;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FealtedByEsra.PL.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }


        [Authorize]
        public IActionResult GetCategory()
        {
            try
            {
                if (User!.Identity!.IsAuthenticated)
                {                    
                    var categories = _categoryService.GetAll(false);
                   
                    _logger.LogInformation("Kategori sayfasına erişildi.");
                    return View(categories);
                }
                else
                {
                    _logger.LogWarning("Kimliği doğurlanmayan kullanıcı kategori sayfasına erişmeye çalıştı.");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Kategori sayfasında beklenmeyen hata {ex}.", ex);
                throw;
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddCategory()
        {
            _logger.LogInformation("Kategori ekleme sayfasına erişildi.");
            return View();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory(CreateCategoryViewModel model)
        {
            try
            {
                Category category = new()
                {
                    CategoryName = model.CategoryName,
                    CreatedDate = model.CreatedDate
                };
                CategoryValidation validations = new();
                ValidationResult result =  validations.Validate(category);
                if (result.IsValid)
                {
                    await _categoryService.AddAsync(category);
                    TempData["AddCategory"] = "Başarıyla Güncellendi!";
                    _logger.LogInformation($"Kategori başarıyla eklendi {category.CategoryName}");
                    return RedirectToAction(nameof(GetCategory));
                }
                else
                {
                    result.AddToModelState(this.ModelState);
                    return View("AddCategory", model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Kategori ekleme sayfasında beklenmeyen hata {ex}", ex);
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditCategory(int id)
        {
            try
            {
                if (User.Identity!.IsAuthenticated)
                {
                    var category = await _categoryService.GetByIdAsync(id, false);
                    return View(category);
                }
                else
                {
                    _logger.LogWarning("Kimliği doğurlanmayan kullanıcı kategori sayfasına erişmeye çalıştı.");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Kategori güncelleme sayfasında beklenmeyen hata {ex}", ex);
                throw;
            }
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(int id, UpdateCategoryViewModel model)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(id, true);
                category.Id = model.Id;
                category.CategoryName = model.CategoryName;
                CategoryValidation validations = new();
                ValidationResult result = validations.Validate(category);
                if (result.IsValid)
                {
                    _categoryService.Update(category);
                    _logger.LogInformation($"Kategori başarıyla güncellendi {model.CategoryName}");
                    TempData["UpdateCategory"] = "Başarıyla Güncellendi!";
                    return RedirectToAction(nameof(GetCategory));
                }
                else
                    return View(category);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Kategori güncelleme sırasında beklenmeyen hata{ex}", ex);
                throw;
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(id);
                if (category == null)
                    return NotFound();
                await _categoryService.RemoveAsync(id);
                _logger.LogInformation($"Kategori başarıyla silindi {category.CategoryName}");
                TempData["DeleteCategory"] = $" {category.CategoryName} başarıyla silindi!.";
                return RedirectToAction("GetCategory", new { page = 1 });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Post silme sırasında beklenmeyen hata {ex}.", ex);
                return BadRequest();
            }
        }
    }
}

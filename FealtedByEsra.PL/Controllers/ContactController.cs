using FealtedByEsra.BAL.Abstract.Services.ContactService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace FealtedByEsra.PL.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly ILogger<ContactController> _logger;
        public ContactController(IContactService contactService, ILogger<ContactController> logger)
        {
            _contactService = contactService;
            _logger = logger;
        }

        [Authorize]
        public IActionResult GetContact()
        {
            try
            {
                if (User!.Identity!.IsAuthenticated)
                {
                    var contact = _contactService.GetAll(false);
                    return View(contact);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Mesaj sayfasında beklenmeyen hata {ex}", ex);
                throw;
            }
        }

        [Authorize]
        public async Task<IActionResult> GetContactId(int id)
        {
            try
            {
                if (User!.Identity!.IsAuthenticated)
                {
                    var contact = await _contactService.GetByIdAsync(id, false);
                    return View(contact);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Mesaj detay sayfasında beklenmeyen hata {ex}", ex);
                throw;
            }
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {                
                var post = await _contactService.GetByIdAsync(id);
                if (post == null)
                    return NotFound();
                await _contactService.RemoveAsync(id);
                TempData["DeleteContact"] = $" {post.Title} başarıyla silindi!.";
                return RedirectToAction("GetContact", new { page = 1 });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Post silme sırasında beklenmeyen hata {ex}.", ex);
                return BadRequest();
            }
        }
    }
}

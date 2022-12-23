using FealtedByEsra.BAL.Abstract.Services;
using FealtedByEsra.BAL.Abstract.Services.GoogleService;
using FealtedByEsra.BAL.Abstract.Services.MailService;
using FealtedByEsra.BAL.Validations.NewsletterValidate;
using FealtedByEsra.Entity.Entities;
using FealtedByEsra.PL.Models.Newsletter;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace FealtedByEsra.PL.Controllers
{
    public class NewsletterController : Controller
    {
        private readonly INewsletterService _newsletter;
        private readonly ILogger<NewsletterController> _logger;
        private readonly IGoogleService _googleService;
        private readonly IEmailService _emailService;

        public NewsletterController(INewsletterService newsletter, ILogger<NewsletterController> logger, IGoogleService googleService, IEmailService emailService)
        {
            _newsletter = newsletter;
            _logger = logger;
            _googleService = googleService;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<JsonResult> SubscribeMail(NewsletterViewModel model)
        {
            try
            {
                var captchaResult = await _googleService.VertifyToken(model.Token);

                if (!captchaResult)
                {
                    return new JsonResult(BadRequest("Hata!"));
                }

                Newsletter newsletter = new()
                {
                    Email = model.Email
                };

                if (_newsletter.GetWhere(n => n.Email.Contains(newsletter.Email)) != null)
                {
                    ModelState.TryAddModelError("Code", $"{model.Email} email zaten abone!");
                }

                NewsletterValidation validation = new();

                ValidationResult result = await validation.ValidateAsync(newsletter);

                if (result.IsValid)
                {
                    await _newsletter.AddAsync(newsletter);
                    try
                    {
                        _emailService.FirstEmail(newsletter.Email, "Esrarga Abone Olundu!", newsletter.Id);
                    }
                    catch (Exception ex)
                    {
                        TempData["er"] = "Hata: " + ex.Message;
                    }
                    ViewBag.nwsSide = newsletter;
                    _logger.LogInformation($"Haber bültenine yeni kayıt => {newsletter.Email}.");
                    return new JsonResult(Ok(newsletter));
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                    return Json(BadRequest());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Haber bülteni kayıt esnasından beklenmeyen hata {ex}.", ex);
                throw;
            }
        }

        [HttpPost]
        public async Task<JsonResult> SubscribeMailFooter(NewsletterViewModel model)
        {
            try
            {
                var captchaResult = await _googleService.VertifyToken(model.Token);
                if (!captchaResult)
                {
                    return new JsonResult(BadRequest());
                }

                Newsletter newsletter = new()
                {
                    Email = model.Email
                };

                NewsletterValidation validation = new();

                ValidationResult result = await validation.ValidateAsync(newsletter);

                if (result.IsValid)
                {
                    await _newsletter.AddAsync(newsletter);
                    try
                    {
                        _emailService.FirstEmail(newsletter.Email, "Esrarga Abone Olundu!", newsletter.Id);
                    }
                    catch (Exception ex)
                    {
                        TempData["er"] = "Hata: " + ex.Message;
                    }
                    TempData["nwsFooter"] = "Abone olundu!";
                    _logger.LogInformation($"Haber bültenine yeni kayıt => {newsletter.Email}.");

                    return new JsonResult(newsletter);
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                    return new JsonResult(BadRequest());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Haber bülteni kayıt esnasından beklenmeyen hata {ex}.", ex);
                throw;
            }
        }

        public async Task<IActionResult> RemoveNewsletter(int id)
        {
            try
            {
                var newsletter = await _newsletter.GetByIdAsync(id);
                if (newsletter == null)
                    return NotFound();
                await _newsletter.RemoveAsync(id);
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Post silme sırasında beklenmeyen hata {ex}.", ex);
                return BadRequest();
            }
        }
    }
}

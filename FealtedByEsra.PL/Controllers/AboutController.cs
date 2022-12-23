using FealtedByEsra.BAL.Abstract.Services.ContactService;
using FealtedByEsra.BAL.Abstract.Services.GoogleService;
using FealtedByEsra.BAL.Abstract.Services.MailService;
using FealtedByEsra.BAL.Filters;
using FealtedByEsra.BAL.Validations.ContactValidate;
using FealtedByEsra.Entity.Entities;
using FealtedByEsra.PL.Models.Contact;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace FealtedByEsra.PL.Controllers
{
    public class AboutController : Controller
    {
        private readonly IContactService _contactService;
        private readonly ILogger<AboutController> _logger;
        private readonly IGoogleService _googleService;
        private readonly IEmailService _emailService;

        public AboutController(IContactService contactService, ILogger<AboutController> logger, IGoogleService googleService, IEmailService emailService)
        {
            _contactService = contactService;
            _logger = logger;
            _googleService = googleService;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ContactForm()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ContactForm(ContactViewModel model)
        {
            try
            {
                var capcthaResult = await _googleService.VertifyToken(model.Token);
                if (!capcthaResult)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return View(model);

                Contact contact = new()
                {
                    NameSurname = model.NameSurname,
                    Email = model.Email,
                    Title = model.Title,
                    Description = model.Description,
                    CreatedDate = model.CreatedDate
                };

                ContactValidation validations = new();

                ValidationResult result = await validations.ValidateAsync(contact);

                if (result.IsValid)
                {
                    await _contactService.AddAsync(contact);

                    try
                    {
                        _emailService.SendEmail(model.NameSurname!, model.Title, model.Email!, model.Description);
                    }
                    catch (Exception ex)
                    {
                        TempData["sendmailerror"] = "Mail gönderilemedi. " + ex.Message;
                    }

                    TempData["sucContact"] = "Başarılı";
                    _logger.LogInformation($"Yeni bir mesaj geldi => {contact.Title.ToUpper()}");
                    return RedirectToAction("Index", "About");
                }
                else
                {
                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Mesaj sırasında beklenmeyen hata => {ex.Message}", ex);
                throw;
            }
        }
    }
}

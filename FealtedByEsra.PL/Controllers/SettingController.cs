using FealtedByEsra.Entity.Entities.Identity;
using FealtedByEsra.PL.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FealtedByEsra.PL.Controllers
{
    [Authorize]
    public class SettingController : Controller
    {
        readonly SignInManager<AppUser> _signInManager;
        readonly UserManager<AppUser> _userManager;

        readonly ILogger<SettingController> _logger;
        public SettingController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ILogger<SettingController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }
        public async Task<IActionResult> Settings()
        {
            AppUser user = await _userManager.FindByNameAsync(User!.Identity!.Name);
            AdminViewModel model = new()
            {
                NameSurname = user.NameSurname,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return View(model);
        }

        public IActionResult EditAccount()
        {
            AppUser user = _userManager.FindByNameAsync(User!.Identity!.Name).Result;

            AdminViewModel model = new()
            {
                NameSurname = user.NameSurname,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> EditAccount(AdminViewModel model)
        {
            if (ModelState.IsValid)
            {

                AppUser user = await _userManager.FindByNameAsync(User!.Identity!.Name);

                string phone = await _userManager.GetPhoneNumberAsync(user);

                if (phone != model.PhoneNumber)
                {
                    if (_userManager.Users.Any(x => x.PhoneNumber == model.PhoneNumber))
                    {
                        ModelState.AddModelError("", "Telefon numarası kayıtlıdır.");
                        return View(model);
                    }
                }

                user.UserName = model.UserName;
                user.Email = model.Email;
                user.NameSurname = model.NameSurname;
                user.PhoneNumber = model.PhoneNumber;

                IdentityResult result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);

                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(user, true);
                    TempData["edituser"] = "Başarılı";
                    _logger.LogInformation("Admin kullanıcısı bilgilerini güncelledi");
                    return RedirectToAction("Settings", "Setting");
                }
                else
                    foreach (var item in result.Errors)
                        ModelState.AddModelError("", item.Description);
            }

            return View(model);
        }


        public IActionResult ChangePassword()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(PasswordChangeAdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByNameAsync(User!.Identity!.Name);

                bool exist = await _userManager.CheckPasswordAsync(user, model.OldPassword);

                if (exist)
                {
                    IdentityResult result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.PasswordConfirm);
                    if (result.Succeeded)
                    {
                        await _userManager.UpdateSecurityStampAsync(user);

                        await _signInManager.SignOutAsync();
                        await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

                        TempData["editpassword"] = "Başarılı";
                        _logger.LogInformation("Admin kullanıcısı şifre bilgilerini güncelledi");
                        return RedirectToAction("Settings");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Eski şifreniz yanlış");
                }
            }
            return View(model);
        }
    }
}

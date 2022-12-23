using FealtedByEsra.Entity.Entities.Identity;
using FealtedByEsra.PL.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace FealtedByEsra.PL.Controllers
{
    public class UserController : Controller
    {
        readonly SignInManager<AppUser> _signInManager;
        readonly UserManager<AppUser> _userManager;
        private readonly ILogger<UserController> _logger;

        public UserController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ILogger<UserController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInUserViewModel user)
        {
            if (ModelState.IsValid)
            {
                if (_userManager.Users.Any(u => u.PhoneNumber == user.PhoneNumber))
                {
                    ModelState.AddModelError("", "Telefon numarası kayıtlıdır.");
                    return View(user);
                }

                AppUser appUser = new();
                appUser.Id = Guid.NewGuid().ToString();
                appUser.NameSurname = user.NameSurname;
                appUser.UserName = user.UserName;
                appUser.Email = user.Email;
                appUser.PhoneNumber = user.PhoneNumber;

                IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Auth");
                }
                else
                    foreach (IdentityError item in result.Errors)
                        ModelState.AddModelError("", item.Description);

            }
            return View(user);
        }

        public IActionResult Login()
        {
            try
            {
                _logger.LogInformation("Giriş sayfasına erişildi.");
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Giriş sayfasında beklenmeyen hata {ex}", ex);
                throw;
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel userViewModel, string? returnUrl = "/")
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        AppUser appUser = await _userManager.FindByNameAsync(userViewModel.Username);
                        try
                        {
                            if (appUser != null)
                            {
                                try
                                {
                                    if (await _userManager.IsLockedOutAsync(appUser))
                                    {
                                        ModelState.AddModelError("", "Hesabınız bir süreliğine kitlenmiştir. Lütfen sonra tekrar deneyin");

                                        return View(userViewModel);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogInformation("Error Login -> Hesap kitlendi!", ex);
                                }

                                await _signInManager.SignOutAsync();
                                var result = await _signInManager.PasswordSignInAsync(appUser, userViewModel.Password, userViewModel.RememberMe, false);

                                if (result.Succeeded)
                                {

                                    if (!string.IsNullOrEmpty(returnUrl))
                                    {
                                        if (Url.IsLocalUrl(returnUrl))
                                        {
                                            TempData["logIn"] = "Başarılı";
                                            return Redirect(returnUrl);
                                        }
                                        else
                                        {
                                            return Redirect("/");
                                        }
                                    }

                                    TempData["logIn"] = "Başarılı";
                                    await _userManager.ResetAccessFailedCountAsync(appUser);

                                    return RedirectToAction("Index", "Home");
                                }
                                else
                                {
                                    try
                                    {

                                        await _userManager.AccessFailedAsync(appUser);


                                        int fail = await _userManager.GetAccessFailedCountAsync(appUser);

                                        ModelState.AddModelError("", $"{fail} kez başarısız giriş");

                                        if (fail == 3)
                                        {
                                            await _userManager.SetLockoutEndDateAsync(appUser, new System.DateTimeOffset(DateTime.Now.AddMinutes(20)));
                                            ModelState.AddModelError("", "Hesabınız geçici süreliğine kitlenmiştir. Lütfen daha sonra tekrar deneyin");
                                        }
                                        else
                                        {
                                            ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış");
                                        }
                                        return RedirectToAction("Index");
                                    }
                                    catch (Exception ex)
                                    {
                                        _logger.LogError("Error Login -> Giriş başarısız!", ex);
                                    }
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", "Kullanıcı bulunamadı");
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError("Error Login -> Kullanıcı bulunamadı!", ex);
                        }

                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Error Login -> Model State hatası!", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Login -> Beklenmeyen hata!", ex);
            }
            return View(userViewModel);
        }

        public async Task LogOut()
        {
            try
            {
                await _signInManager.SignOutAsync();
                TempData["logout"] = "Başarılı";
                Log.Information("Kullanıcı çıkış yaptı");
            }
            catch (Exception ex)
            {
                Log.Error("Çıkış hatalı", ex);
            }
        }
    }
}

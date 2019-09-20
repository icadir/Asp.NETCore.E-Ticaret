using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using ShopApp.WebUI.Extensions;
using ShopApp.WebUI.Identity;
using ShopApp.WebUI.Models;

namespace ShopApp.WebUI.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IEmailSender _emailSender;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }
        public IActionResult Register()
        {
            return View(new RegisterModel());
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FullName = model.FullName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                //generate token 
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id,
                    token = code
                });
                // send email
                await _emailSender.SendEmailAsync(model.Email, "Hesabınızı Onaylayın", $"Lütfen Email Hesabınızı Onaylamak için like <a href='http://localhost:12344{callbackUrl}'tıklayınız.</a>");

                TempData.Put("message", new ResultMessage
                {
                    Title="Hesap Onayı",
                    Message="Eposta Adresinize Gelen Link İle Hesabınızı Onaylayınız.",
                    Css="warning"
                });



                return RedirectToAction("login", "account");
            }
            ModelState.AddModelError("", "Bilinmeyen hata olustu lütfen tekrar deneriniz.");
            return View();
        }

        public IActionResult Login(string returnUrl = null)
        {

            return View(new LoginModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Bu Kullanıcı ile Daha Önce Hesal Olusturulmamıstır.");
                return View(model);
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Lütfen Hesabınızı Mail ile Onaylayınız.");
                return View(model);
            }


            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl ?? "~/");
            }
            ModelState.AddModelError("", "Email  veya parola yanlıs.");

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData.Put("message", new ResultMessage
            {
                Title = "Oturum Kapatıldı",
                Message = "Hesabınız Güvenli Bir Şekilde Sonlarıldı.",
                Css = "warning"
            });
            return Redirect("~/");
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                TempData.Put("message", new ResultMessage
                {
                    Title = "Hesap Onayı",
                    Message = "Hesap Onayı İçin Bilgileriniz Yanlış.",
                    Css = "danger"
                });
                return Redirect("~/");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    TempData.Put("message", new ResultMessage
                    {
                        Title = "Hesap Onayı",
                        Message = "Hesabınız Onaylandı..",
                        Css = "success"
                    });
                    return RedirectToAction("Login");
                }
            }
            TempData.Put("message", new ResultMessage
            {
                Title = "Hesap Onayı",
                Message = "Hesabınız Onaylanamadı..",
                Css = "danger"
            });
            return View();
        }

        public IActionResult ForgotPassword()
        {
            TempData.Put("message", new ResultMessage
            {
                Title = "Forgot Password",
                Message = "Bilgileriniz hatalı.",
                Css = "success"
            });
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                return View();
            }
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                TempData.Put("message", new ResultMessage
                {
                    Title = "Forgot Password",
                    Message = "E-posta Adresi ile bir kullanıcı bulunamadı..",
                    Css = "success"
                });
                return View();
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callbackUrl = Url.Action("ResetPassword", "Account", new
            {
                userId = user.Id,
                token = code
            });
            // send email
            await _emailSender.SendEmailAsync(Email, "Reset Password", $"Parolanızı yenilenemek için  like <a href='http://localhost:12344{callbackUrl}'tıklayınız.</a>");
            TempData.Put("message", new ResultMessage
            {
                Title = "Forgot Password",
                Message = "Parola Yenilemek için Hesabınıza Parola Gönderild..",
                Css = "warning"
            });
            return RedirectToAction("login", "account");
        }

        public IActionResult ResetPassword(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Home", "Index");
            }
            var model = new ResetPasswordModel
            {
                Token = token
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction("Home", "Index");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }
    }
}
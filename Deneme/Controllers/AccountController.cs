using Deneme.Context;
using Deneme.ViewModels;
using Deneme.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Deneme.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var securityAnswerHash = _userManager.PasswordHasher.HashPassword(new ApplicationUser(), model.SecurityAnswer);

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                SecurityQuestion = model.SecurityQuestion,
                SecurityAnswerHash = securityAnswerHash,
                CreatedDate = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
                return RedirectToAction("Login", "Account");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        // Giriş Sayfası
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
                return RedirectToAction("Dashboard", "Admin");

            ModelState.AddModelError("", "Giriş başarısız.");
            return View(model);
        }

        
        // Şifremi Unuttum Sayfası
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordWithSecurityViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı.");
                return View(model);
            }

            var result = _userManager.PasswordHasher.VerifyHashedPassword(user, user.SecurityAnswerHash, model.SecurityAnswer);
            if (result != PasswordVerificationResult.Success)
            {
                ModelState.AddModelError("", "Güvenlik cevabı hatalı.");
                return View(model);
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return RedirectToAction("ResetPassword", new { email = user.Email, token });
        }

        // Şifre Sıfırlama Sayfası
        public IActionResult ResetPassword(string email, string token)
        {
            var model = new ResetPasswordViewModel
            {
                Email = email,
                Token = token
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı.");
                return View(model);
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (result.Succeeded)
                return RedirectToAction("Login", "Account");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
     
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
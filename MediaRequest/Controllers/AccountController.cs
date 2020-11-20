using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaRequest.WebUI.Models.IdentityModels;
using MediaRequest.WebUI.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediaRequest.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IdentityContext _identityContext;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IdentityContext identityContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _identityContext = identityContext;
        }

        public IActionResult Login(string ReturnUrl)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string ReturnUrl)
        {
            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ApplicationUser input)
        {
            if(ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);

                if (input.Email != currentUser.Email || input.UserName != currentUser.UserName)
                {
                    var contextUser = await _identityContext.AspNetUsers.SingleOrDefaultAsync(x => x.Id == currentUser.Id);

                    if (input.Email != currentUser.Email)
                    {
                        contextUser.Email = input.Email;
                    }

                    if (input.UserName != currentUser.UserName)
                    {
                        contextUser.Email = input.Email;
                    }

                    await _identityContext.SaveChangesAsync();
                }
            }

            TempData["Success"] = "Information updated succesfully!";
            return RedirectToAction("Profile", "User");
        }
    }
}
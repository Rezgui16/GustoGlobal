using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gusto.Models;
using GustoLib.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gusto.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    DateNaissance = model.DateNaissance,
                    LastName = model.LastName,
                    FirstMidName = model.FirstMidName
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }

                else
                {
                    ModelState.AddModelError("", result.ToString());
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, String returnURL)
        {
            if (ModelState.IsValid)
            {
                var resultat = await signInManager.PasswordSignInAsync(model.Email,
                    model.Password,
                    model.RememberMe,
                    false);
                if (resultat.Succeeded)
                {
                    if (!string.IsNullOrWhiteSpace(returnURL))
                        return Redirect(returnURL);
                    return RedirectToAction("index", "home");
                }
                if (resultat.IsLockedOut)
                {
                    ModelState.AddModelError("", "Le compte est bloqué");
                }
                else
                {
                    ModelState.AddModelError("", "Email / mot de passe invalide");
                }

            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
    }
}
﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OCC.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Authentication;
using System.Linq;
using System.Text.Json;

// AccountController used to validate credentials for Users Accounts
// Users can select if they corresponds to Cleaner or Customer
// If Cleaner's credentials validattion succeed, user will be addres to 
// customized HomePage (return RedirectToAction("Index", "Home") in the Login Action;

namespace Users.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private ICleanerRepository cleanerRepository;
        private ICustomerRepository customerRepository;

        public AccountController(UserManager<AppUser> userMgr,
                SignInManager<AppUser> signinMgr, ICleanerRepository cleanerRepo
            , ICustomerRepository customerRepo)
        {
            userManager = userMgr;
            signInManager = signinMgr;
            cleanerRepository = cleanerRepo;
            customerRepository = customerRepo;
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel details)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByEmailAsync(details.Email);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result =
                            await signInManager.PasswordSignInAsync(
                                user, details.Password, false, false);
                    if (result.Succeeded)
                    {
                        switch (details.UserType)
                        {
                            case "Cleaner":
                                Cleaner cleaner = cleanerRepository.Cleaners.FirstOrDefault(o => o.Email == details.Email);
                              
                                ////Serialize the order information in order to send to the next controller
                                byte[] jsonUser = JsonSerializer.SerializeToUtf8Bytes(cleaner);
                                HttpContext.Session.Set("cleaner", jsonUser);
                                return RedirectToAction("Index", "Home");

                            case "Customer":
                                Customer customer = customerRepository.Customers.FirstOrDefault(o => o.Email == details.Email);
                                return RedirectToAction("RegisteredCleanerDetails", "Cleaner");

                            default:
                                break;

                        }
                    }
                }
                ModelState.AddModelError(nameof(LoginModel.Email),
                    "Invalid user or password");
            }
            return View(details);
        }
        public ViewResult CallHome()
        {
            byte[] value;
            bool isValueAvailable = HttpContext.Session.TryGetValue("cleaner", out value);

            if (isValueAvailable)
            {
                Cleaner cleaner = JsonSerializer.Deserialize<Cleaner>(value);
                return View("Views/Home/Index", cleaner);
            }
            return View("Views/Home/Index", new Cleaner());
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult GoogleLogin(string returnUrl)
        {
            string redirectUrl = Url.Action("GoogleResponse", "Account",
                new { ReturnUrl = returnUrl });
            var properties = signInManager
                .ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> GoogleResponse(string returnUrl = "/")
        {
            ExternalLoginInfo info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }
            var result = await signInManager.ExternalLoginSignInAsync(
                info.LoginProvider, info.ProviderKey, false);
            if (result.Succeeded)
            {
                return Redirect(returnUrl);
            }
            else
            {
                AppUser user = new AppUser
                {
                    Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    UserName =
                        info.Principal.FindFirst(ClaimTypes.Email).Value
                };
                IdentityResult identResult = await userManager.CreateAsync(user);
                if (identResult.Succeeded)
                {
                    identResult = await userManager.AddLoginAsync(user, info);
                    if (identResult.Succeeded)
                    {
                        await signInManager.SignInAsync(user, false);
                        return Redirect(returnUrl);
                    }
                }
                return AccessDenied();
            }
        }
    }
}

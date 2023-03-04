using ePizza.Entities.Concrete;
using ePizza.Repositories.Models;
using ePizza.Services.Interfaces;
using ePizza.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace ePizza.UI.Controllers
{
    public class AccountController : Controller
    {
        private IAuthenticationService _authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = _authenticationService.AuthenticateUser(model.Email, model.Password);
                if (user !=null)
                {
                    if (!string.IsNullOrEmpty(returnUrl)&&Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    if (user.Roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Dashboard", new {area = "Admin"});
                    }
                    else if (user.Roles.Contains("User"))
                    {
                        return RedirectToAction("Index", "Dashboard", new { area = "User" });
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Eposta adresiniz veya şifreniz yanlıştır.");
                    ViewBag.Error = "Eposta veya şifreniz yanlıştır.";
                    return View("Login");
                }
            }
            else
            {
                ModelState.AddModelError("", "Eposta veya şifreniz yanlıştır.");
            }
            return View("Login");
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                User user = new User
                {
                    Name = userViewModel.Name,
                    UserName = userViewModel.Email,
                    Email = userViewModel.Email,
                    PhoneNumber = userViewModel.PhoneNumber,
                };
                bool result = _authenticationService.CreateUser(user, userViewModel.Password);
                if (result)
                {
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult LogOutComplete()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Unauthorize()
        {
            return View();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Modelcasepro.RequestModel;
using Modelcasepro.ViewModel;
using Domaincasepro.Commands;
using Domaincasepro.Queries;

namespace Webcasepro.Controllers
{
    public class AccountController : Controller
    {
        private readonly LoginQueryHandler _loginHandler;
        private readonly LoginCommandHandler _loginCommandHandler;

        public AccountController(LoginQueryHandler loginHandler, LoginCommandHandler loginCommandHandler)
        {
            _loginHandler = loginHandler;
            _loginCommandHandler = loginCommandHandler;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var loginResult = _loginHandler.ExecuteLoginQuery(user);
                    var response = _loginCommandHandler.Execute(user, loginResult);
                    if (response.Success)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.Username),
                            // Add more claims as needed
                        };

                        var userIdentity = new ClaimsIdentity(claims, "login");
                        var principal = new ClaimsPrincipal(userIdentity);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            principal, new AuthenticationProperties
                            {
                                // IsPersistent = true, // Set 'true' if you want persistent authentication
                                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30) // Set expiration time
                            });

                        // Authentication successful
                        // Redirect to home page or any other page
                        ViewBag.ErrorMessage = response.Message;
                        return RedirectToAction("CreateActivity", "Activity");
                    }
                    else
                    {
                        // Authentication failed
                        ViewBag.ErrorMessage = response.Message;
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while authorizing the user: " + ex.Message);
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }
    }
}





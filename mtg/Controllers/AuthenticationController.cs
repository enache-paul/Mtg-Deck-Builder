using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using mtg_lib.Services;
using mtg.Models;

namespace mtg.Controllers;

public class AuthenticationController : Controller
{
    private readonly ILogger<AuthenticationController> _logger;
    private UsersService service;

    public AuthenticationController(ILogger<AuthenticationController> logger)
    {
        _logger = logger;
        service = new UsersService();
    }
    
    public IActionResult Login()
    {
        ClaimsPrincipal claimUser = HttpContext.User;

        if (claimUser.Identity != null && claimUser.Identity.IsAuthenticated)
            return RedirectToAction("Index", "Home");
        
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(AuthViewModel modelLogin)
    {
        if (ValidateCredentials(modelLogin))
        {
            modelLogin.Id = service.GetUserId(modelLogin.Email).ToString();
            
            List<Claim> claims = new List<Claim> { new Claim(ClaimTypes.Sid, modelLogin.Id)};

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme );

            AuthenticationProperties properties = new AuthenticationProperties() {
                AllowRefresh = true,
                IsPersistent = modelLogin.KeepLoggedIn
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity), properties);
            
            return RedirectToAction("Index", "Home");
        }
        
        return View(modelLogin);
    }

    private bool ValidateCredentials(AuthViewModel viewModel)
    {
        return (ModelState.IsValid) ? service.AuthenticateUser(viewModel.Email, viewModel.Password) : false;
    }

    [HttpPost]
    public IActionResult Register(RegistrationViewModel modelRegister)
    {
        if (ModelState.IsValid)
        {
            if (service.CreateNewUser(modelRegister.Email, modelRegister.Password))
            {
                return RedirectToAction("Login", "Authentication");
            }
        }
        return View(modelRegister);
    }

    public IActionResult Register()
    {
        ClaimsPrincipal claimUser = HttpContext.User;

        if (claimUser.Identity != null && claimUser.Identity.IsAuthenticated)
            return RedirectToAction("Index", "Home");
        
        return View();
    }
}
﻿using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.JsonWebTokens;
using RentVilla.MVC.DTOs;
using RentVilla.MVC.Models.Account;
using RentVilla.MVC.Models.Address;
using RentVilla.MVC.Services;
using System.Security.Claims;

namespace RentVilla.MVC.Controllers
{
    public class AccountController : Controller
    {
        public INotyfService _notifyService { get; }
        private readonly IConfiguration _configuration;

        public AccountController(INotyfService notifyService, IConfiguration configuration)
        {
            _notifyService = notifyService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {

            RegisterVM model = new();
            List<StateVM>? response = new();
            string baseUrl = _configuration["API:Url"];
            using (HttpClient httpClient = new())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage responseApi = await httpClient.GetAsync("Region/GetAllStates");
                string contentResponseApi = await responseApi.Content.ReadAsStringAsync();
                response = System.Text.Json.JsonSerializer.Deserialize<List<StateVM>>(contentResponseApi);
            }
            model.UserAddress = new UserAddressVM()
            {
                States = response.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
         {
            string baseUrl = _configuration["API:Url"];
            PostUserAddressVM postUserAddressVM = new()
            {
                StateId = model.UserAddress.SelectedStateId,
                CityId = model.UserAddress.SelectedCityId,
                DistrictId = model.UserAddress.SelectedDistrictId
            };
            PostRegisterVM postRegisterVM = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                BirthDate = model.BirthDate,
                Gender = model.Gender,
                Password = model.Password,
                PasswordConfirm = model.PasswordConfirm,
                ProfileImage = model.ProfileImage,
                UserAddress = postUserAddressVM
            };
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage responseApi = httpClient.PostAsJsonAsync("users/createuser", postRegisterVM
                    ).Result;
                if (responseApi.IsSuccessStatusCode)
                {
                    _notifyService.Success("Registration successful. You can now login.");
                    return RedirectToAction("Login");
                }
                else
                {
                    _notifyService.Error("An error occurred while registering. Please try again.");
                    return RedirectToAction("Register");
                }
            }
        }
                
        public IActionResult Login(string returnUrl = null)
        {
            if(returnUrl != null)
            {
                TempData["ReturnUrl"] = returnUrl;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            string baseUrl = _configuration["API:Url"];
              LoginResponseVM loginResponseModel = new();
            if (ModelState.IsValid)
            { 
              using (HttpClient client = new HttpClient())
              {
                    client.BaseAddress = new Uri(baseUrl);
                    HttpResponseMessage responseApi = await client.PostAsJsonAsync("auth/login", model);
               
                    string contentResponseApi = await responseApi.Content.ReadAsStringAsync();
                    loginResponseModel = System.Text.Json.JsonSerializer.Deserialize<LoginResponseVM>(contentResponseApi);
                    
                    if (!string.IsNullOrEmpty(loginResponseModel?.Token.AccessToken) || loginResponseModel?.Token.Expiration > DateTime.UtcNow)
                    {
                        await TokenCookieHandler(loginResponseModel);

                        if (loginResponseModel.Token.RefreshToken != null)
                        HttpContext.Response.Cookies.Append("RentVilla.Cookie_RT", loginResponseModel?.Token.RefreshToken);

                        var returnUrl = TempData["ReturnUrl"]?.ToString();
                        _notifyService.Success("You are successfully logged in. Enjoy your stay!");
                        if (!String.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        _notifyService.Error("An error occurred while logging in. Please try again.");
                        return View();
                    }
                }
            }
            else
            {
                _notifyService.Error("An error occurred while logging in. Please try again.");
                return View();
            }
        }
        public async Task<IActionResult> Logout()
        {
            HttpContext.Response.Cookies.Delete("RentVilla.Cookie");
            HttpContext.Response.Cookies.Delete("RentVilla.Cookie_RT");
            TempData["ReturnUrl"] = null;
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _notifyService.Success("You are successfully logged out. See you soon!");
            return Redirect("~/");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        [NonAction]
        public async Task TokenCookieHandler(LoginResponseVM model)
        {
            
            var handler = new JsonWebTokenHandler();

            var jsonToken = handler.ReadToken(model.Token.AccessToken) as JsonWebToken;

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Hash, model.Token.AccessToken),
                    new Claim(ClaimTypes.Name, model.UserData.UserName),
                    new Claim(ClaimTypes.Expiration, model.Token.Expiration.ToString()),
                    new Claim("RefreshToken", model.Token.RefreshToken.ToString()),
                    new Claim(ClaimTypes.Role, "Administrator")
                };
            var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = model.Token.Expiration,
                IsPersistent = true,
                AllowRefresh = true
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(userIdentity), authProperties);
        }
    }
}
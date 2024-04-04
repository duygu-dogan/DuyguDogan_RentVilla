using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RentVilla.MVC.Models.Account;
using RentVilla.MVC.Models.Address;
using System.Security.Claims;
using System.Text.Json;

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
              TokenVM? tokenModel = new();
            if (ModelState.IsValid)
            { 
              using (HttpClient client = new HttpClient())
              {
                    client.BaseAddress = new Uri(baseUrl);
                    HttpResponseMessage responseApi = await client.PostAsJsonAsync("auth/login", model);
               
                    string contentResponseApi = await responseApi.Content.ReadAsStringAsync();
                    tokenModel = System.Text.Json.JsonSerializer.Deserialize<TokenVM>(contentResponseApi);
                    
                    if (!string.IsNullOrEmpty(tokenModel?.Token.AccessToken) || tokenModel.Token.Expiration > DateTime.UtcNow)
                    {
                        var handler = new JsonWebTokenHandler();
                       
                        var jsonToken = handler.ReadToken(tokenModel?.Token.AccessToken) as JsonWebToken;
                       
                        var userName = jsonToken?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;;
                        var role = jsonToken?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
                        var expireAt = jsonToken?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Expiration)?.Value;
                        
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Hash, tokenModel.Token.AccessToken),
                            new Claim(ClaimTypes.Name, userName),
                            new Claim(ClaimTypes.Expiration, expireAt)
                            //new Claim(ClaimTypes.Role, role)
                        };
                        var userIdentity = new ClaimsIdentity("Custom");
                        userIdentity.AddClaims(claims);

                       //authProperties.IsPersistent = model.RememberMe;
                       await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(userIdentity), new AuthenticationProperties
                        {
                            ExpiresUtc = tokenModel.Token.Expiration,
                            IsPersistent = false,
                            AllowRefresh = false
                        });

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

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Response.Cookies.Delete("RentVilla.Cookie");
            TempData["ReturnUrl"] = null;
            _notifyService.Success("You are successfully logged out. See you soon!");
            return Redirect("~/");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

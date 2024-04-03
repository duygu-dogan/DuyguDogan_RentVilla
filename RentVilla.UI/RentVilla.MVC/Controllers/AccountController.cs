using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;
using RentVilla.MVC.Models.Account;
using RentVilla.MVC.Models.Address;
using System.Net.Http;
using System.Runtime.InteropServices;
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
                response = JsonSerializer.Deserialize<List<StateVM>>(contentResponseApi);
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
                HttpResponseMessage responseApi = httpClient.PostAsJsonAsync("users/adduser", postRegisterVM
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
                
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            string baseUrl = _configuration["API:Url"];
            TokenVM? tokenModel = new();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage responseApi = await client.PostAsJsonAsync("users/login", model);
                string contentResponseApi = await responseApi.Content.ReadAsStringAsync();
                tokenModel = JsonSerializer.Deserialize<TokenVM>(contentResponseApi);
                if (tokenModel.Token.AccessToken != null)
                {
                    _notifyService.Success("You are successfully logged in. Enjoy your stay!");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    _notifyService.Error("An error occurred while logging in. Please try again.");
                    return RedirectToAction("Login");
                }
            }
        }
    }
}

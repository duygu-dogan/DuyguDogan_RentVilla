using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using RentVilla.MVC.Helpers.ErrorHandling;
using RentVilla.MVC.Services.HttpClientService;
using RentVilla.MVC.Services.TokenCookieService;

namespace RentVilla.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<ITokenCookieHandlerService, TokenCookieHandlerService>();
            builder.Services.AddScoped<IHttpClientService, HttpClientService>();

            builder.Services.AddControllersWithViews();
            builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.Cookie = new Microsoft.AspNetCore.Http.CookieBuilder
                { 
                        Name = "RentVilla.Cookie",
                        SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict
                    };
                
            });
            
            builder.Services.AddHttpContextAccessor();


            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            var cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
            };
            app.UseCookiePolicy(cookiePolicyOptions);

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseRouting();

            

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapFallbackToController("Index", "Admin");

            app.UseNotyf();
            app.Run();
        }
    }
}

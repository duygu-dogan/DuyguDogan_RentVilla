using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RentVilla.MVC.Helpers.ErrorHandling;
using RentVilla.MVC.Helpers.TokenHandling;
using RentVilla.MVC.Services.HttpClientService;
using RentVilla.MVC.Services.TokenCookieService;
using System.Text;

namespace RentVilla.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<ITokenCookieHandlerService, TokenCookieHandlerService>();

            builder.Services.AddControllersWithViews();
            builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer("Admin", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SigningKey"])),
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["Token:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["Token:Audience"],
                        ValidateLifetime = true
                    };
                });

            //builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie("Cookies", options =>
            //    {
            //        //options.LoginPath = "/Account/Login";
            //        options.LogoutPath = "/Account/Logout";
            //        options.AccessDeniedPath = "/Account/AccessDenied";
            //        options.Cookie = new Microsoft.AspNetCore.Http.CookieBuilder
            //        {
            //            HttpOnly = true,
            //            Name = "RentVilla.Cookie",
            //            SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict
            //        };
            //    });
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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<TokenExpirationValidationMiddleware>();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapFallbackToController("Index", "Admin");

            app.UseNotyf();
            app.Run();
        }
    }
}

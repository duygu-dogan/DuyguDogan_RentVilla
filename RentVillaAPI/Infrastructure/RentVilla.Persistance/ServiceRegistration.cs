using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RentVilla.Application.Abstraction.Services;
using RentVilla.Application.Repositories.AttributeRepo;
using RentVilla.Application.Repositories.EndpointRepo;
using RentVilla.Application.Repositories.FileRepo;
using RentVilla.Application.Repositories.MenuRepo;
using RentVilla.Application.Repositories.ProductRepo;
using RentVilla.Application.Repositories.RegionRepo;
using RentVilla.Application.Repositories.ReservationCartItemRepo;
using RentVilla.Application.Repositories.ReservationCartRepo;
using RentVilla.Application.Repositories.ReservationRepo;
using RentVilla.Domain.Entities.Concrete.Identity;
using RentVilla.Persistance.Contexts;
using RentVilla.Persistence.Configs;
using RentVilla.Persistence.Repositories.AttributeCRepo;
using RentVilla.Persistence.Repositories.EndpointCRepo;
using RentVilla.Persistence.Repositories.FileCRepo;
using RentVilla.Persistence.Repositories.MenuCRepo;
using RentVilla.Persistence.Repositories.ProductCRepo;
using RentVilla.Persistence.Repositories.RegionCRepo;
using RentVilla.Persistence.Repositories.RepoCRepo;
using RentVilla.Persistence.Repositories.ResCartItemRepo;
using RentVilla.Persistence.Repositories.ResCartRepo;
using RentVilla.Persistence.Repositories.ReservationCRepo;
using RentVilla.Persistence.Services;

namespace RentVilla.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(options => 
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;  
            }).AddEntityFrameworkStores<RentVillaDbContext>();

            
            services.AddDbContext<RentVillaDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString));

            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            services.AddScoped<IAttributeReadRepository, AttributeReadRepository>();
            services.AddScoped<IAttributeWriteRepository, AttributeWriteRepository>();
            services.AddScoped<IAttributeTypeWriteRepository, AttributeTypeWriteRepository>();
            services.AddScoped<IAttributeTypeReadRepository, AttributeTypeReadRepository>();
            services.AddScoped<IProductAttributeReadRepository, ProductAttributeReadRepository>();
            services.AddScoped<IProductAttributeWriteRepository, ProductAttributeWriteRepository>();

            services.AddScoped<IReservationReadRepository, ReservationReadRepository>();
            services.AddScoped<IReservationWriteRepository, ReservationWriteRepository>();

            services.AddScoped<ICountryReadRepository, CountryReadRepository>();
            services.AddScoped<IStateReadRepository, StateReadRepository>();
            services.AddScoped<ICityReadRepository, CityReadRepository>();
            services.AddScoped<IDistrictReadRepository, DistrictReadRepository>();
            services.AddScoped<IProductAddressReadRepository, ProductAddressReadRepository>();
            services.AddScoped<IProductAddressWriteRepository, ProductAddressWriteRepository>();

            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IFileWriteRepository, FileWriteRepository>();
            services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
            services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();
            services.AddScoped<IStateImageFileWriteRepository, StateImageFileWriteRepository>();
            services.AddScoped<IStateImageFileReadRepository, StateImageFileReadRepository>();

            services.AddScoped<IResCartReadRepository, ResCartReadRepository>();
            services.AddScoped<IResCartWriteRepository, ResCartWriteRepository>();
            services.AddScoped<IResCartItemReadRepository, ResCartItemReadRepository>();
            services.AddScoped<IResCartItemWriteRepository, ResCartItemWriteRepository>();

            services.AddScoped<IEndpointWriteRepository, EndpointWriteRepository>();
            services.AddScoped<IEndpointReadRepository, EndpointReadRepository>();
            services.AddScoped<IMenuReadRepository, MenuReadRepository>();
            services.AddScoped<IMenuWriteRepository, MenuWriteRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IRoleService, RoleService>();
        }
    }
}

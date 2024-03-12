using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentVilla.Application.Abstract.Services;
using RentVilla.Persistance.Concrete.Services;
using RentVilla.Persistance.Contexts;
using RentVilla.Persistence.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<RentVillaDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString));
            services.AddSingleton<IProductService, ProductService>();
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using RentVilla.Application.Abstraction.Services.AuthConfigurations;
using RentVilla.Application.Abstraction.Storage;
using RentVilla.Application.Abstraction.Token;
using RentVilla.Domain.Entities.Concrete.Identity;
using RentVilla.Infrastructure.Enums;
using RentVilla.Infrastructure.Services;
using RentVilla.Infrastructure.Services.AuthConfiguration;
using RentVilla.Infrastructure.Services.Storage;
using RentVilla.Infrastructure.Services.Storage.Azure;
using RentVilla.Infrastructure.Services.Storage.Local;
using RentVilla.Infrastructure.Services.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IStorageService, StorageService>();
            serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
            serviceCollection.AddScoped<IAuthConfigService, AuthConfigService>();

        }
        public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : Storage, IStorage
        {
            serviceCollection.AddScoped<IStorage, T>();
        }
        //public static void AddStorage(this IServiceCollection serviceCollection, StorageType storageType) 
        //{
        //    switch(storageType)
        //    {
        //        case StorageType.Azure:
        //            serviceCollection.AddScoped<IStorage, AzureStorage>();
        //            break;
        //        case StorageType.Local:
        //            serviceCollection.AddScoped<IStorage, LocalStorage>();
        //            break;
        //        case StorageType.AWS:
        //            break;
        //        default:
        //            serviceCollection.AddScoped<IStorage, LocalStorage>();
        //            break;
        //    }
        //}
    }
}

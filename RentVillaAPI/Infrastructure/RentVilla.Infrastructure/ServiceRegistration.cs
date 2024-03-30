﻿using Microsoft.Extensions.DependencyInjection;
using RentVilla.Application.Abstraction.Storage;
using RentVilla.Infrastructure.Enums;
using RentVilla.Infrastructure.Services;
using RentVilla.Infrastructure.Services.Storage;
using RentVilla.Infrastructure.Services.Storage.Azure;
using RentVilla.Infrastructure.Services.Storage.Local;
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
﻿using InventoryMgmt.Service.Service_Interface;
using InventoryMgmt.Service;

namespace InventoryMgmt.DependencyInjections
{
    public static class InventoryManagementServicesDependency
    {
        public static void RegisterAppServices(this IServiceCollection services)
        {
            services.AddScoped<IItemService, ItemService>();
            services.AddTransient<IValidation, Validation>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IStoreService, StoreService>();
            services.AddTransient<IReusableLogic, ReusableLogic>();
            services.AddTransient<ISalesService, SalesService>();
            services.AddTransient<IStockService, StockService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddTransient<IEmailService, EmailService>();
        }

    }
}

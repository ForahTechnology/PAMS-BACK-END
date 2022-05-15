using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PAMS.Application.Interfaces.Persistence;
using PAMS.Application.Interfaces.Services;
using PAMS.Application.Interfaces.Utilities;
using PAMS.Domain.Common;
using PAMS.Persistence.Context;
using PAMS.Persistence.Implementation;
using PAMS.Services.Implementations;
using PAMS.Services.Implementations.Utilites;
using PAMS.Services.Implementations.Utilities;
using System;

namespace PAMS.Persistence
{
    /// <summary>
    /// This class registers all services for easy and clean registration in the Startup class.
    /// Ensure that all services are registered here.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// A static method for service registration in the Startup.cs Class.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PAMSdbContext>(options => options
                    .UseSqlServer(configuration.GetConnectionString("PAMSDBConnection"),
                    b => b
                    .MigrationsAssembly(typeof(PAMSdbContext).Assembly.FullName)));

            services.AddScoped<IPAMSdbContext>(provider => provider.GetService<PAMSdbContext>());
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped(typeof(IStoreManager<>), typeof(StoreManager<>));
            services.AddScoped<IMailer, Mailer>();
            services.AddScoped<IMailService, SendGridMailService>();
            services.AddScoped<ISampleService, SampleService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<ISettingsService, SettingsService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<INotification, Notification>();
            services.AddScoped<IAnalysisService, AnalysisService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IFieldScientistAnalysisNesreaService, FieldScientistAnalysisNesreaService>();
            services.AddScoped<IFieldScientistAnalysisDPRService, FieldScientistAnalysisDPRService>();
            services.AddScoped<IFieldScientistAnalysisFMEnvService, FieldScientistAnalysisFMEnvService>();
            services.AddScoped<IUploadService, UploadService>();
            services.AddScoped<IFileStorageService, FileStorageService>();
            services.AddScoped<IDashBoardService, DashBoardService>();
        }
    }
}

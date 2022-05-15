using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PAMS.Application.Interfaces;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());            
        }
    }
}


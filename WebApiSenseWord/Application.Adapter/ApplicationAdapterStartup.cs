using Application.Adapter.SenseWord;
using Application.Adapter.SenseWord.Interfaces;
using Infrastructure.Sql;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Adapter
{
    public static class ApplicationAdapterStartup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            InfrastructureSqlStartup.ConfigureServices(services);

            services.AddSingleton<ISenseWordAdapter, SenseWordAdapter>();
        }
    }
}

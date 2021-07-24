using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiSenseWord.Mappers;
using WebApiSenseWord.Mappers.Interfaces;

namespace WebApiSenseWord
{
    public static class WebApiSenseWordStartup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ISenseWordMapper, SenseWordMapper>();
        }
    }
}

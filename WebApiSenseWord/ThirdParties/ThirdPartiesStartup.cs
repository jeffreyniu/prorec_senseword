using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using ThirdParties.Authentication;
using ThirdParties.Authentication.Interfaces;
using ThirdParties.HttpHandlers;
using ThirdParties.HttpHandlers.Interface;

namespace ThirdParties
{
    public static class ThirdPartiesStartup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ITokenAuth, TokenAuth>();
            services.AddSingleton<IHttpPostHandler, HttpPostHandler>();
        }
    }
}

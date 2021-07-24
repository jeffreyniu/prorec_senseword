using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Adapter;
using CustomCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ThirdParties;
using WebApiSenseWord.Mappers;
using WebApiSenseWord.Mappers.Interfaces;

namespace WebApiSenseWord
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            ThirdPartiesStartup.ConfigureServices(services);
            ApplicationAdapterStartup.ConfigureServices(services);

            WebApiSenseWordStartup.ConfigureServices(services);

            // Add functionality to inject IOptions<T>
            services.AddOptions();

            // Add AppSettings object so that it can be injected
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

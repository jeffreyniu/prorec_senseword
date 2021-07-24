using Infrastructure.Entity;
using Infrastructure.Sql.Statements.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Sql
{
    public static class InfrastructureSqlStartup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(IDatabaseContext<>), typeof(DatabaseContext<>));
            services.AddSingleton<ISenseWordStatement, SenseWordStatement>();
        }
    }
}

﻿using Empresa.Dapper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Dapper.API.Configuration
{
    public static class DataBaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase(databaseName: "DbFake"));

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Connection")));
        }

        public static void UseDatabaseConfiguration(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
        }
    }
}
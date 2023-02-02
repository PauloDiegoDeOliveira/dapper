using Empresa.Dapper.API.Extensions;
using Empresa.Dapper.Application.Applications;
using Empresa.Dapper.Application.Interfaces;
using Empresa.Dapper.Domain.Core.Interfaces.Repositories;
using Empresa.Dapper.Domain.Core.Interfaces.Service;
using Empresa.Dapper.Domain.Core.Notificacoes;
using Empresa.Dapper.Domain.Services;
using Empresa.Dapper.Infrastructure.Data.Repositorys.Dapper;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Empresa.Dapper.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IParticipanteRepository, ParticipanteRepository>();
            services.AddScoped<IParticipanteApplication, ParticipanteApplication>();
            services.AddScoped<IParticipanteService, ParticipanteService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUser, AspNetUser>();
            services.AddScoped<INotificador, Notificador>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        }
    }
}
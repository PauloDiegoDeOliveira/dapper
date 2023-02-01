using Empresa.Dapper.Application.Validations.Participante;
using FluentValidation;
using FluentValidation.AspNetCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Empresa.Dapper.API.Configuration
{
    public static class FluentValidationConfig
    {
        public static void AddFluentValidationConfiguration(this IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(config =>
                {
                    config.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    config.SerializerSettings.Converters.Add(new StringEnumConverter());
                })
                .AddJsonOptions(conf =>
                {
                    conf.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.AddValidatorsFromAssemblyContaining<PostParticipanteValidator>();

            services.AddFluentValidationAutoValidation();

            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-BR");
        }
    }
}
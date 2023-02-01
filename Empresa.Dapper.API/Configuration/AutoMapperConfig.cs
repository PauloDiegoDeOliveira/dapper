using Empresa.Dapper.Application.Mappers;

namespace Empresa.Dapper.API.Configuration
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(ParticipanteMappingProfile));
        }
    }
}
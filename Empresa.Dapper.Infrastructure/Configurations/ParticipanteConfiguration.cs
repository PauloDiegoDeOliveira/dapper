using Empresa.Dapper.Domain.Entitys;
using Empresa.Dapper.Infrastructure.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Empresa.Dapper.Infrastructure.Configurations
{
    public class ParticipanteConfiguration : ConfigurationBase<Participante>
    {
        public override void Configure(EntityTypeBuilder<Participante> builder)
        {
            tableName = "Participantes";

            base.Configure(builder);
        }
    }
}
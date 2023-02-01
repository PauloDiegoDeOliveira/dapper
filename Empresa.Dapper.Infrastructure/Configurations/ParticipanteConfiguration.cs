using Empresa.Dapper.Domain.Entitys;
using Empresa.Dapper.Infrastructure.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Empresa.Dapper.Infrastructure.Configurations
{
    public class ParticipanteConfiguration : ConfigurationBase<Participante>
    {
        public override void Configure(EntityTypeBuilder<Participante> builder)
        {
            tableName = "Participantes";

            base.Configure(builder);

            builder.Property(p => p.Nome)
                   .IsRequired()
                   .HasColumnName("Nome")
                   .HasMaxLength(150)
                   .HasColumnType("varchar(150)");

            builder.Property(p => p.Sobrenome)
                   .HasColumnName("Sobrenome")
                   .HasMaxLength(150)
                   .HasColumnType("varchar(150)");

            builder.Property(p => p.CPF)
                   .HasColumnName("CPF")
                   .HasMaxLength(50)
                   .HasColumnType("varchar(50)");
        }
    }
}
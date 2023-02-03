using Empresa.Dapper.Domain.Entitys;
using Empresa.Dapper.Infrastructure.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Empresa.Dapper.Infrastructure.Configurations
{
    public class ProdutoConfiguration : ConfigurationBase<Produto>
    {
        public override void Configure(EntityTypeBuilder<Produto> builder)
        {
            tableName = "Produtos";

            base.Configure(builder);

            builder.Property(p => p.Nome)
                   .IsRequired()
                   .HasColumnName("Nome")
                   .HasMaxLength(150)
                   .HasColumnType("varchar(150)");
        }
    }
}
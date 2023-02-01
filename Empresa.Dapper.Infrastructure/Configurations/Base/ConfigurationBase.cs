﻿using Empresa.Dapper.Domain.Entitys.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Empresa.Dapper.Infrastructure.Configurations.Base
{
    public class ConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : EntityBase
    {
        protected string tableName;

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(tableName);

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Status)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasColumnName("Status")
                   .HasColumnType("varchar(50)")
                   .HasDefaultValue("Ativo");
        }
    }
}
﻿using Empresa.Dapper.Domain.Entitys.Base;

namespace Empresa.Dapper.Application.Structs
{
    public struct EntityToDto<TEntity, TDto> where TEntity : EntityBase where TDto : class
    {
        public TEntity Entity { get; private set; }

        public TDto Dto { get; private set; }

        public EntityToDto(TEntity entity, TDto dto)
        {
            Entity = entity;
            Dto = dto;
        }
    }
}
﻿using Empresa.Dapper.Domain.Entitys.Base;

namespace Empresa.Dapper.Domain.Entitys
{
    public class Participante : EntityBase
    {
        public string Nome { get; private set; }

        public string Sobrenome { get; private set; }

        public string CPF { get; private set; }
    }
}
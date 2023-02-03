using Empresa.Dapper.Infrastructure.Data.Repositorys.Dapper.ScriptsSql.Base;

namespace Empresa.Dapper.Infrastructure.Data.Repositorys.Dapper.ScriptsSql.Participante
{
    public class ParticipanteScript : IDapperScriptBase
    {
        public string GetAll()
        {
            return @"Select distinct
                                                id,
                                                Nome,
                                                Sobrenome,
                                                cpf,
                                                Status,
                                                CriadoEm,
                                                AlteradoEm
                                        From Participantes";
        }

        public string GetById()
        {
            return @"Select distinct
                                                id,
                                                Nome,
                                                Sobrenome,
                                                cpf,
                                                Status,
                                                CriadoEm,
                                                AlteradoEm
                                        From Participantes
                                            Where id = @id";
        }
    }
}
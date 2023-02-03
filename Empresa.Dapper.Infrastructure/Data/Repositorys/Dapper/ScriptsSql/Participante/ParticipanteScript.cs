namespace Empresa.Dapper.Infrastructure.Data.Repositorys.Dapper.ScriptsSql.Participante
{
    public class ParticipanteScript
    {
        public const string GetAll = @"Select distinct
                                              id,
                                              Nome,
                                              Sobrenome,
                                              cpf,
                                              Status,
                                              CriadoEm,
                                              AlteradoEm
                                           From Participantes";

        public const string GetById = @"Select distinct
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
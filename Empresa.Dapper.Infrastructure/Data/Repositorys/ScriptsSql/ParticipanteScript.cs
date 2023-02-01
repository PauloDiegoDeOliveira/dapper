namespace Empresa.Dapper.Infrastructure.Data.Repositorys.ScriptsSql
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
    }
}
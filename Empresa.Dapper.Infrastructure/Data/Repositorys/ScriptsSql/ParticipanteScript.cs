namespace Empresa.Dapper.Infrastructure.Data.Repositorys.ScriptsSql
{
    public class ParticipanteScript
    {
        public static string GetAll
        {
            get
            {
                return @"SELECT DISTINCT
                                ID,
                                NOME,
                                SOBRENOME,
                                CPF,
                                STATUS,
                                CRIADOEM,
                                ALTERADOEM
                        FROM Participantes";
            }
        }
    }
}
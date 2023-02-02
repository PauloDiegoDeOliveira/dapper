using Dapper.FluentMap.Dommel.Mapping;
using Empresa.Dapper.Domain.Entitys;

namespace Empresa.Dapper.Application.Mappers.Dapper
{
    public class ParticipanteDapperMap : DommelEntityMap<Participante>
    {
        public ParticipanteDapperMap()
        {
            ToTable("Participantes");

            Map(participante => participante.Id).IsKey();
        }
    }
}
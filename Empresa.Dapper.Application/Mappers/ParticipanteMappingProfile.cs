using AutoMapper;
using Empresa.Dapper.Application.Dtos.Participante;
using Empresa.Dapper.Domain.Entitys;

namespace Empresa.Dapper.Application.Mappers
{
    public class ParticipanteMappingProfile : Profile
    {
        public ParticipanteMappingProfile()
        {
            Map();
        }

        private void Map()
        {
            CreateMap<PostParticipanteDto, Participante>().ReverseMap();
            CreateMap<PutParticipanteDto, Participante>().ReverseMap();
            CreateMap<Participante, ViewParticipanteDto>().ReverseMap();
        }
    }
}
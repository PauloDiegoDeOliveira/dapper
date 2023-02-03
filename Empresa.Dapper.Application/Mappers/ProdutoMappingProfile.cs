using AutoMapper;
using Empresa.Dapper.Application.Dtos.Produto;
using Empresa.Dapper.Domain.Entitys;

namespace Empresa.Dapper.Application.Mappers
{
    public class ProdutoMappingProfile : Profile
    {
        public ProdutoMappingProfile()
        {
            Map();
        }

        private void Map()
        {
            CreateMap<PostProdutoDto, Produto>().ReverseMap();
            CreateMap<PutProdutoDto, Produto>().ReverseMap();
            CreateMap<Produto, ViewProdutoDto>().ReverseMap();
        }
    }
}
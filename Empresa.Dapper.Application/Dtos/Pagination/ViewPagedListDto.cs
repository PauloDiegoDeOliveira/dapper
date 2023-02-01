using Empresa.Dapper.Domain.Entitys.Base;
using Empresa.Dapper.Domain.Pagination;

namespace Empresa.Dapper.Application.Dtos.Pagination
{
    public class ViewPagedListDto<TEntity, TView> where TEntity : EntityBase where TView : class
    {
        public ICollection<TView> Pagina { get; set; }

        public ViewPaginationDataDto<TEntity> Dados { get; set; }

        public ViewPagedListDto(PagedList<TEntity> pagedList, List<TView> list)
        {
            Pagina = list;
            Dados = new ViewPaginationDataDto<TEntity>(pagedList);
        }
    }
}
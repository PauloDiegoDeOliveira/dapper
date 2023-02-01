namespace Empresa.Dapper.Domain.Pagination
{
    public class PagedList<T> : List<T>
    {
        public int PaginaAtual { get; private set; }

        public int TotalPaginas { get; private set; }

        public int ResultadosExibidosPagina { get; private set; }

        public int ContagemTotalResultados { get; private set; }

        public bool ExistePaginaAnterior => PaginaAtual > 1 && TotalPaginas > 1;

        public bool ExistePaginaPosterior => PaginaAtual < TotalPaginas;

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            ContagemTotalResultados = count;
            ResultadosExibidosPagina = pageSize;
            PaginaAtual = pageNumber;
            TotalPaginas = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public static PagedList<T> ToPagedList(IQueryable<T> query, int pageNumber, int pageSize)
        {
            int count = query.Count();

            var items = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
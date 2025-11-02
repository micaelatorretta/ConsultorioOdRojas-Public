using Portable.FunctionalUnits.Prestaciones.DTOs;
using Portable.FunctionalUnits.Prestaciones.Responses;
using Shared.Portable.Base;
using Shared.Portable.Pagination;

namespace Portable.FunctionalUnits.Prestaciones.Queries
{
    /// <summary>
    /// Query paginada, para obtener todos los datos de Nomenclador.
    /// </summary>
    public class GetNomencladorPagedQuery : BasePagedQuery<NomencladorDTO, GetNomencladorPagedResponse>
    {
        public GetNomencladorPagedQuery(PaginationParams paginationParams) : base(paginationParams) { }
      
    }
}
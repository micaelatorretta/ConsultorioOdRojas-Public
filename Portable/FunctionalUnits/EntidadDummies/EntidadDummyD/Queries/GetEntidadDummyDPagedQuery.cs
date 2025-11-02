using Portable.FunctionalUnits.EntidadDummies.DTOs;
using Portable.FunctionalUnits.EntidadDummies.Responses;
using Shared.Portable.Base;
using Shared.Portable.Pagination;

namespace Portable.FunctionalUnits.EntidadDummies.Queries
{
    /// <summary>
    /// Query paginada, para obtener todos los datos de EntidadDummy.
    /// </summary>
    public class GetEntidadDummyDPagedQuery : BasePagedQuery<EntidadDummyDDTO, GetEntidadDummyDPagedResponse>
    {
        public GetEntidadDummyDPagedQuery(PaginationParams paginationParams) : base(paginationParams) { }
      
    }
}
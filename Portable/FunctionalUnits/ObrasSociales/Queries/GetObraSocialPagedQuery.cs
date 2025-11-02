using Portable.FunctionalUnits.ObrasSociales.DTOs;
using Portable.FunctionalUnits.ObrasSociales.Responses;
using Shared.Portable.Base;
using Shared.Portable.Pagination;

namespace Portable.FunctionalUnits.ObrasSociales.Queries
{
    /// <summary>
    /// Query paginada, para obtener todos los datos de ObraSocial.
    /// </summary>
    public class GetObraSocialPagedQuery : BasePagedQuery<ObraSocialDTO, GetObraSocialPagedResponse>
    {
        public GetObraSocialPagedQuery(PaginationParams paginationParams) : base(paginationParams) { }
      
    }
}
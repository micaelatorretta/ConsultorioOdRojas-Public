using Portable.FunctionalUnits.Odontogramas.DTOs;
using Portable.FunctionalUnits.Odontogramas.Responses;
using Shared.Portable.Base;
using Shared.Portable.Pagination;

namespace Portable.FunctionalUnits.Odontogramas.Queries
{
    /// <summary>
    /// Query paginada, para obtener todos los datos de Odontograma.
    /// </summary>
    public class GetOdontogramaPagedQuery : BasePagedQuery<OdontogramaDTO, GetOdontogramaPagedResponse>
    {
        public GetOdontogramaPagedQuery(PaginationParams paginationParams) : base(paginationParams) { }
      
    }
}
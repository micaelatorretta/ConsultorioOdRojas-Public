using Portable.FunctionalUnits.Odontogramas.DTOs;
using Portable.FunctionalUnits.Odontogramas.Responses;
using Shared.Portable.Base;
using Shared.Portable.Pagination;

namespace Portable.FunctionalUnits.Odontogramas.Queries
{
    /// <summary>
    /// Query paginada, para obtener todos los datos de PiezaDental.
    /// </summary>
    public class GetPiezaDentalPagedQuery : BasePagedQuery<PiezaDentalDTO, GetPiezaDentalPagedResponse>
    {
        public GetPiezaDentalPagedQuery(PaginationParams paginationParams) : base(paginationParams) { }
      
    }
}
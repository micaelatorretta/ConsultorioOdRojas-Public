using Portable.FunctionalUnits.Odontogramas.DTOs;
using Portable.FunctionalUnits.Odontogramas.Responses;
using Shared.Portable.Base;
using Shared.Portable.Pagination;

namespace Portable.FunctionalUnits.Odontogramas.Queries
{
    /// <summary>
    /// Query paginada, para obtener todos los datos de CaraDental.
    /// </summary>
    public class GetCaraDentalPagedQuery : BasePagedQuery<CaraDentalDTO, GetCaraDentalPagedResponse>
    {
        public GetCaraDentalPagedQuery(PaginationParams paginationParams) : base(paginationParams) { }
      
    }
}
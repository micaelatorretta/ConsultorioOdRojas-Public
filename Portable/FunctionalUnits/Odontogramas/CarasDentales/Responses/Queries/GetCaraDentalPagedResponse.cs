using Portable.FunctionalUnits.Odontogramas.DTOs;
using Shared.Portable.Base;
using Shared.Portable.Pagination;

namespace Portable.FunctionalUnits.Odontogramas.Responses
{
    public class GetCaraDentalPagedResponse : BasePagedResponse<CaraDentalDTO>
    {
        public GetCaraDentalPagedResponse() : base() { }
        public GetCaraDentalPagedResponse(PaginationData<CaraDentalDTO> paginationData)
            : base(paginationData) { }
    }
}

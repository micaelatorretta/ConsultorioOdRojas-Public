using Portable.FunctionalUnits.Odontogramas.DTOs;
using Shared.Portable.Base;
using Shared.Portable.Pagination;

namespace Portable.FunctionalUnits.Odontogramas.Responses
{
    public class GetPiezaDentalPagedResponse : BasePagedResponse<PiezaDentalDTO>
    {
        public GetPiezaDentalPagedResponse() : base() { }
        public GetPiezaDentalPagedResponse(PaginationData<PiezaDentalDTO> paginationData)
            : base(paginationData) { }
    }
}

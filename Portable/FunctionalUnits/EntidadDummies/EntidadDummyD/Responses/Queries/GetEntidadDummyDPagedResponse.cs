using Portable.FunctionalUnits.EntidadDummies.DTOs;
using Shared.Portable.Base;
using Shared.Portable.Pagination;

namespace Portable.FunctionalUnits.EntidadDummies.Responses
{
    public class GetEntidadDummyDPagedResponse : BasePagedResponse<EntidadDummyDDTO>
    {
        public GetEntidadDummyDPagedResponse() : base() { }
        public GetEntidadDummyDPagedResponse(PaginationData<EntidadDummyDDTO> paginationData)
            : base(paginationData) { }
    }
}

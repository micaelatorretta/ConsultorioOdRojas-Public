using Portable.FunctionalUnits.EntidadDummies.DTOs;
using Shared.Portable.Base;
using Shared.Portable.Pagination;

namespace Portable.FunctionalUnits.EntidadDummies.Responses
{
    public class GetEntidadDummyPagedResponse : BasePagedResponse<EntidadDummyDTO>
    {
        public GetEntidadDummyPagedResponse() : base() { }
        public GetEntidadDummyPagedResponse(PaginationData<EntidadDummyDTO> paginationData)
            : base(paginationData) { }
    }
}

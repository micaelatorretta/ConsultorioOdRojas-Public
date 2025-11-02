using Portable.FunctionalUnits.ObrasSociales.DTOs;
using Shared.Portable.Base;
using Shared.Portable.Pagination;

namespace Portable.FunctionalUnits.ObrasSociales.Responses
{
    public class GetObraSocialPagedResponse : BasePagedResponse<ObraSocialDTO>
    {
        public GetObraSocialPagedResponse() : base() { }
        public GetObraSocialPagedResponse(PaginationData<ObraSocialDTO> paginationData)
            : base(paginationData) { }
    }
}

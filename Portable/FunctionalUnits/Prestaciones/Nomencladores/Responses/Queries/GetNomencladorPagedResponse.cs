using Portable.FunctionalUnits.Odontogramas.DTOs;
using Portable.FunctionalUnits.Prestaciones.DTOs;
using Shared.Portable.Base;
using Shared.Portable.Pagination;

namespace Portable.FunctionalUnits.Prestaciones.Responses
{
    public class GetNomencladorPagedResponse : BasePagedResponse<NomencladorDTO>
    {
        public GetNomencladorPagedResponse() : base() { }
        public GetNomencladorPagedResponse(PaginationData<NomencladorDTO> paginationData)
            : base(paginationData) { }
    }
}

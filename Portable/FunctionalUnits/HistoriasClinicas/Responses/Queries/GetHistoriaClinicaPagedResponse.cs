using Portable.FunctionalUnits.HistoriasClinicas.DTOs;
using Shared.Portable.Base;
using Shared.Portable.Pagination;

namespace Portable.FunctionalUnits.HistoriasClinicas.Responses
{
    public class GetHistoriaClinicaPagedResponse : BasePagedResponse<HistoriaClinicaDTO>
    {
        public GetHistoriaClinicaPagedResponse() : base() { }
        public GetHistoriaClinicaPagedResponse(PaginationData<HistoriaClinicaDTO> paginationData)
            : base(paginationData) { }
    }
}

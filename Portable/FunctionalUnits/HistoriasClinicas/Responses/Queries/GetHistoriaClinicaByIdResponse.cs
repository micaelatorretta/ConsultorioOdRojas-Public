using Portable.FunctionalUnits.HistoriasClinicas.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.HistoriasClinicas.Responses
{
    public class GetHistoriaClinicaByIdResponse : BaseResponse
    {
        public GetHistoriaClinicaByIdResponse() { }

        public HistoriaClinicaDTO HistoriaClinica { get; set; } = null!;
    }
}
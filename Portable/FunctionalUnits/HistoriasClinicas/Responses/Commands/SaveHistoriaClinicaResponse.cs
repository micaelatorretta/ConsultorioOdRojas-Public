using Portable.FunctionalUnits.HistoriasClinicas.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.HistoriasClinicas.Responses
{
    public class SaveHistoriaClinicaResponse : BaseResponse
    {
        public SaveHistoriaClinicaResponse()
        {

        }

        public HistoriaClinicaDTO? HistoriaClinica { get; set; } 
    }
}
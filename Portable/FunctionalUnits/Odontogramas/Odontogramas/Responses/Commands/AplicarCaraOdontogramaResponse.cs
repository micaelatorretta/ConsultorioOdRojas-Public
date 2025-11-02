using Portable.FunctionalUnits.Odontogramas.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.Odontogramas.Responses
{
    public class AplicarCaraOdontogramaResponse : BaseResponse
    {
        public AplicarCaraOdontogramaResponse()
        {

        }

        public OdontogramaDTO? Odontograma { get; set; } 
    }
}
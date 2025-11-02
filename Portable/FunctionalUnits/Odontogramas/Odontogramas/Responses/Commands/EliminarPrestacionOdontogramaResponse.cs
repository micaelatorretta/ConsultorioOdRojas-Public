using Portable.FunctionalUnits.Odontogramas.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.Odontogramas.Responses
{
    public class EliminarPrestacionOdontogramaResponse : BaseResponse
    {
        public EliminarPrestacionOdontogramaResponse()
        {

        }

        public OdontogramaDTO? Odontograma { get; set; } 
    }
}
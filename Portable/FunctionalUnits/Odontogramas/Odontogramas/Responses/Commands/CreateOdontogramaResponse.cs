using Portable.FunctionalUnits.Odontogramas.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.Odontogramas.Responses
{
    public class CreateOdontogramaResponse : BaseResponse
    {
        public CreateOdontogramaResponse()
        {

        }

        public OdontogramaDTO? Odontograma { get; set; } 
    }
}
using Portable.FunctionalUnits.Odontogramas.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.Odontogramas.Responses
{
    public class UpdateOdontogramaResponse : BaseResponse
    {
        public UpdateOdontogramaResponse()
        {

        }

        public OdontogramaDTO? Odontograma { get; set; } 
    }
}
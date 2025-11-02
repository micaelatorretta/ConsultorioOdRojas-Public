using Portable.FunctionalUnits.Odontogramas.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.Odontogramas.Responses
{
    public class CreateCaraDentalResponse : BaseResponse
    {
        public CreateCaraDentalResponse()
        {

        }

        public CaraDentalDTO? CaraDental { get; set; } 
    }
}
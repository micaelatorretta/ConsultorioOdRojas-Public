using Portable.FunctionalUnits.Odontogramas.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.Odontogramas.Responses
{
    public class UpdateCaraDentalResponse : BaseResponse
    {
        public UpdateCaraDentalResponse()
        {

        }

        public CaraDentalDTO? CaraDental { get; set; } 
    }
}
using Portable.FunctionalUnits.Odontogramas.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.Odontogramas.Responses
{
    public class CreatePiezaDentalResponse : BaseResponse
    {
        public CreatePiezaDentalResponse()
        {

        }

        public PiezaDentalDTO? PiezaDental { get; set; } 
    }
}
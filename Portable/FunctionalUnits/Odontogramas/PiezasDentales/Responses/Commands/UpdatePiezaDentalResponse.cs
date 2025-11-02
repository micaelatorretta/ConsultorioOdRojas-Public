using Portable.FunctionalUnits.Odontogramas.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.Odontogramas.Responses
{
    public class UpdatePiezaDentalResponse : BaseResponse
    {
        public UpdatePiezaDentalResponse()
        {

        }

        public PiezaDentalDTO? PiezaDental { get; set; } 
    }
}
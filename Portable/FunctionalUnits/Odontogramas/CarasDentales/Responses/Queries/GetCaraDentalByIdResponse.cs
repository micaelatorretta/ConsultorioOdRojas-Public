using Portable.FunctionalUnits.Odontogramas.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.Odontogramas.Responses
{
    public class GetCaraDentalByIdResponse : BaseResponse
    {
        public GetCaraDentalByIdResponse() { }

        public CaraDentalDTO CaraDental { get; set; } = null!;
    }
}
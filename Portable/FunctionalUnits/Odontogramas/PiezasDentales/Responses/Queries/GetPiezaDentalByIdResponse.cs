using Portable.FunctionalUnits.Odontogramas.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.Odontogramas.Responses
{
    public class GetPiezaDentalByIdResponse : BaseResponse
    {
        public GetPiezaDentalByIdResponse() { }

        public PiezaDentalDTO PiezaDental { get; set; } = null!;
    }
}
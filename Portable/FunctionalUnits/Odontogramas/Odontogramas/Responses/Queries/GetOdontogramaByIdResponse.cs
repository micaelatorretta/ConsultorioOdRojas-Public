using Portable.FunctionalUnits.Odontogramas.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.Odontogramas.Responses
{
    public class GetOdontogramaByIdResponse : BaseResponse
    {
        public GetOdontogramaByIdResponse() { }

        public OdontogramaDTO Odontograma { get; set; } = null!;
    }
}
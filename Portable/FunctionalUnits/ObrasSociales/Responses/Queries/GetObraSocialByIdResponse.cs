using Portable.FunctionalUnits.ObrasSociales.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.ObrasSociales.Responses
{
    public class GetObraSocialByIdResponse : BaseResponse
    {
        public GetObraSocialByIdResponse() { }

        public ObraSocialDTO ObraSocial { get; set; } = null!;
    }
}
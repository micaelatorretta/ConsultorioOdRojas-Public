using Portable.FunctionalUnits.ObrasSociales.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.ObrasSociales.Responses
{
    public class UpdateObraSocialResponse : BaseResponse
    {
        public UpdateObraSocialResponse()
        {

        }

        public ObraSocialDTO? ObraSocial { get; set; } 
    }
}
using Portable.FunctionalUnits.EntidadDummies.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.EntidadDummies.Responses
{
    public class UpdateEntidadDummyResponse : BaseResponse
    {
        public UpdateEntidadDummyResponse()
        {

        }

        public EntidadDummyDTO? EntidadDummy { get; set; } 
    }
}
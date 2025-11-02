using Portable.FunctionalUnits.EntidadDummies.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.EntidadDummies.Responses
{
    public class CreateEntidadDummyResponse : BaseResponse
    {
        public CreateEntidadDummyResponse()
        {

        }

        public EntidadDummyDTO? EntidadDummy { get; set; } 
    }
}
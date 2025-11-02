using Portable.FunctionalUnits.EntidadDummies.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.EntidadDummies.Responses
{
    public class GetEntidadDummyDByIdResponse : BaseResponse
    {
        public GetEntidadDummyDByIdResponse() { }

        public EntidadDummyDDTO EntidadDummyD { get; set; } = null!;
    }
}
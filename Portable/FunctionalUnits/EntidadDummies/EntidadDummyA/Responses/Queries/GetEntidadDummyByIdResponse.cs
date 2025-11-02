using Portable.FunctionalUnits.EntidadDummies.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.EntidadDummies.Responses
{
    public class GetEntidadDummyByIdResponse : BaseResponse
    {
        public GetEntidadDummyByIdResponse() { }

        public EntidadDummyDTO EntidadDummy { get; set; } = null!;
    }
}
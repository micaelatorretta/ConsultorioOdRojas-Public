using Portable.FunctionalUnits.EntidadDummies.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.EntidadDummies.Responses
{
    public class CreateEntidadDummyDResponse : BaseResponse
    {
        public CreateEntidadDummyDResponse()
        {

        }

        public EntidadDummyDDTO? EntidadDummyD { get; set; } 
    }
}
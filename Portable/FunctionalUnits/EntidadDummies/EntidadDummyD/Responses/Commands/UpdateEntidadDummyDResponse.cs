using Portable.FunctionalUnits.EntidadDummies.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.EntidadDummies.Responses
{
    public class UpdateEntidadDummyDResponse : BaseResponse
    {
        public UpdateEntidadDummyDResponse()
        {

        }

        public EntidadDummyDDTO? EntidadDummyD { get; set; } 
    }
}
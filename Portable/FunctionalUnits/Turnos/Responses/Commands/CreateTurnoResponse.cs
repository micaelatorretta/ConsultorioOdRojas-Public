using Portable.FunctionalUnits.Turnos.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.Turnos.Responses
{
    public class CreateTurnoResponse : BaseResponse
    {
        public CreateTurnoResponse()
        {

        }

        public TurnoDTO? Turno { get; set; } 
    }
}
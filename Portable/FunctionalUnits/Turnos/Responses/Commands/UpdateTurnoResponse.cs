using Portable.FunctionalUnits.Turnos.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.Turnos.Responses
{
    public class UpdateTurnoResponse : BaseResponse
    {
        public UpdateTurnoResponse()
        {

        }

        public TurnoDTO? Turno { get; set; } 
    }
}
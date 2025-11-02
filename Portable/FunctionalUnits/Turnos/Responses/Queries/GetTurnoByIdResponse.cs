using Portable.FunctionalUnits.Turnos.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.Turnos.Responses
{
    public class GetTurnoByIdResponse : BaseResponse
    {
        public GetTurnoByIdResponse() { }

        public TurnoDTO Turno { get; set; } = null!;
    }
}
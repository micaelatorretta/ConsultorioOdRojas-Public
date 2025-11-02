using Portable.FunctionalUnits.Turnos.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Turnos.Queries
{
    public class GetTurnoByIdQuery : BaseQuery<GetTurnoByIdResponse>
    {
        public GetTurnoByIdQuery() { }
      
        public int Id { get; set; }
    }
}
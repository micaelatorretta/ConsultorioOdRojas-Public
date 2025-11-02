using Domain.FunctionalUnits.Pacientes.Interfaces;
using Domain.FunctionalUnits.Turnos.Entities;
using Domain.FunctionalUnits.Turnos.Interfaces;
using Portable.FunctionalUnits.Turnos.DTOs;
using Portable.FunctionalUnits.Turnos.Queries;
using Portable.FunctionalUnits.Turnos.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Domain.Utils;

namespace Application.FunctionalUnits.Turnos.Queries
{
    public class GetTurnoByIdQueryHandler : BaseQueryHandler<GetTurnoByIdQuery, GetTurnoByIdResponse>
    {

        public GetTurnoByIdQueryHandler(IWorkContext workContext) : base(workContext) { }


        public override async Task<GetTurnoByIdResponse> Handle(GetTurnoByIdQuery query, CancellationToken cancellationToken)
        {
            var response = new GetTurnoByIdResponse();

            var graphFull = new GraphExplorerConfiguration() { Mode = GraphExplorerMode.Full };
            
            var turno = await WorkContext.Services
                                            .ReadOnlyUnitOfWork
                                            .GetRepository<Turno, ITurnoRepository>()
                                            .GetByIdAsync(query.Id);

            response.Turno = WorkContext.Services.Mapper.Map<TurnoDTO>(turno);
            return response;
        }
    }
}

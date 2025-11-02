using Domain.FunctionalUnits.Pacientes.Entities;
using Domain.FunctionalUnits.Pacientes.Interfaces;
using Portable.FunctionalUnits.Pacientes.DTOs;
using Portable.FunctionalUnits.Pacientes.Queries;
using Portable.FunctionalUnits.Pacientes.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Domain.Utils;

namespace Application.FunctionalUnits.Pacientes.Queries
{
    public class GetPacienteByIdQueryHandler : BaseQueryHandler<GetPacienteByIdQuery, GetPacienteByIdResponse>
    {
        
        public GetPacienteByIdQueryHandler(IWorkContext workContext) : base(workContext) { }


        public override async Task<GetPacienteByIdResponse> Handle(GetPacienteByIdQuery query, CancellationToken cancellationToken)
        {
            var response = new GetPacienteByIdResponse();

            var graphFull = new GraphExplorerConfiguration() { Mode = GraphExplorerMode.Full };
            
            var paciente = await WorkContext.Services
                                            .ReadOnlyUnitOfWork
                                            .GetRepository<Paciente, IPacienteRepository>()
                                            .GetByIdAsync(query.Id);

            response.Paciente = WorkContext.Services.Mapper.Map<PacienteDTO>(paciente);
            return response;
        }
    }
}

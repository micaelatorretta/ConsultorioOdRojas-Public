using Domain.FunctionalUnits.Pacientes.Entities;
using Portable.FunctionalUnits.Pacientes.Commands;
using Portable.FunctionalUnits.Pacientes.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;

namespace Application.FunctionalUnits.Pacientes.Commands
{
    public class DeletePacienteCommandHandler : BaseCommandHandler<DeletePacienteCommand, DeletePacienteResponse>
    {

        public DeletePacienteCommandHandler(IWorkContext workContext) : base(workContext)
        {
        }

        public override async Task<DeletePacienteResponse> Handle(DeletePacienteCommand command, CancellationToken cancellationToken)
        {
            var response = new DeletePacienteResponse();

            await em.DeleteAsync<Paciente>(command.Id);

            return response;
        }

       
    }
}

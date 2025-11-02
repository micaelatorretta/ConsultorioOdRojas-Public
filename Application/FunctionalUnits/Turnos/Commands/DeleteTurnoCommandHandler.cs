using Domain.FunctionalUnits.Turnos.Entities;
using Portable.FunctionalUnits.Turnos.Commands;
using Portable.FunctionalUnits.Turnos.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;

namespace Application.FunctionalUnits.Turnos.Commands
{
    public class DeleteTurnoCommandHandler : BaseCommandHandler<DeleteTurnoCommand, DeleteTurnoResponse>
    {

        public DeleteTurnoCommandHandler(IWorkContext workContext) : base(workContext)
        {
        }

        public override async Task<DeleteTurnoResponse> Handle(DeleteTurnoCommand command, CancellationToken cancellationToken)
        {
            var response = new DeleteTurnoResponse();

            await em.DeleteAsync<Turno>(command.Id);

            return response;
        }

       
    }
}

using Domain.FunctionalUnits.Usuarios.Entities;
using Portable.FunctionalUnits.Usuarios.Commands;
using Portable.FunctionalUnits.Usuarios.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;

namespace Application.FunctionalUnits.Usuarios.Commands
{
    public class DeleteUsuarioCommandHandler : BaseCommandHandler<DeleteUsuarioCommand, DeleteUsuarioResponse>
    {

        public DeleteUsuarioCommandHandler(IWorkContext workContext) : base(workContext)
        {
        }

        public override async Task<DeleteUsuarioResponse> Handle(DeleteUsuarioCommand command, CancellationToken cancellationToken)
        {
            var response = new DeleteUsuarioResponse();

            await em.DeleteAsync<Usuario>(command.Id);

            return response;
        }

       
    }
}

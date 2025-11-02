using Domain.FunctionalUnits.ObrasSociales.Entities;
using Portable.FunctionalUnits.ObrasSociales.Commands;
using Portable.FunctionalUnits.ObrasSociales.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;

namespace Application.FunctionalUnits.ObrasSociales.Commands
{
    public class DeleteObraSocialCommandHandler : BaseCommandHandler<DeleteObraSocialCommand, DeleteObraSocialResponse>
    {

        public DeleteObraSocialCommandHandler(IWorkContext workContext) : base(workContext)
        {
        }

        public override async Task<DeleteObraSocialResponse> Handle(DeleteObraSocialCommand command, CancellationToken cancellationToken)
        {
            var response = new DeleteObraSocialResponse();

            await em.DeleteAsync<ObraSocial>(command.Id);

            return response;
        }

       
    }
}

using Domain.FunctionalUnits.Odontogramas.Entities;
using Portable.FunctionalUnits.Odontogramas.Commands;
using Portable.FunctionalUnits.Odontogramas.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;

namespace Application.FunctionalUnits.Odontogramas.Commands
{
    public class DeleteOdontogramaCommandHandler : BaseCommandHandler<DeleteOdontogramaCommand, DeleteOdontogramaResponse>
    {

        public DeleteOdontogramaCommandHandler(IWorkContext workContext) : base(workContext)
        {
        }

        public override async Task<DeleteOdontogramaResponse> Handle(DeleteOdontogramaCommand command, CancellationToken cancellationToken)
        {
            var response = new DeleteOdontogramaResponse();

            await em.DeleteAsync<Odontograma>(command.Id);

            return response;
        }

       
    }
}

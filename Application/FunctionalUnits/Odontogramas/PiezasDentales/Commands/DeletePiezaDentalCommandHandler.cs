using Domain.FunctionalUnits.Odontogramas.Entities;
using Portable.FunctionalUnits.Odontogramas.Commands;
using Portable.FunctionalUnits.Odontogramas.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;

namespace Application.FunctionalUnits.Odontogramas.Commands
{
    public class DeletePiezaDentalCommandHandler : BaseCommandHandler<DeletePiezaDentalCommand, DeletePiezaDentalResponse>
    {

        public DeletePiezaDentalCommandHandler(IWorkContext workContext) : base(workContext)
        {
        }

        public override async Task<DeletePiezaDentalResponse> Handle(DeletePiezaDentalCommand command, CancellationToken cancellationToken)
        {
            var response = new DeletePiezaDentalResponse();

            await em.DeleteAsync<PiezaDental>(command.Id);

            return response;
        }

       
    }
}

using Domain.FunctionalUnits.Odontogramas.Entities;
using Portable.FunctionalUnits.Odontogramas.Commands;
using Portable.FunctionalUnits.Odontogramas.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;

namespace Application.FunctionalUnits.Odontogramas.Commands
{
    public class DeleteCaraDentalCommandHandler : BaseCommandHandler<DeleteCaraDentalCommand, DeleteCaraDentalResponse>
    {

        public DeleteCaraDentalCommandHandler(IWorkContext workContext) : base(workContext)
        {
        }

        public override async Task<DeleteCaraDentalResponse> Handle(DeleteCaraDentalCommand command, CancellationToken cancellationToken)
        {
            var response = new DeleteCaraDentalResponse();

            await em.DeleteAsync<CaraDental>(command.Id);

            return response;
        }

       
    }
}

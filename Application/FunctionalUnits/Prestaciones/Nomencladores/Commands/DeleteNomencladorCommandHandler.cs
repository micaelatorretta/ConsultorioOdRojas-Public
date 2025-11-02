using Domain.FunctionalUnits.Prestaciones.Entities;
using Portable.FunctionalUnits.Prestaciones.Commands;
using Portable.FunctionalUnits.Prestaciones.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;

namespace Application.FunctionalUnits.Prestaciones.Commands
{
    public class DeleteNomencladorCommandHandler : BaseCommandHandler<DeleteNomencladorCommand, DeleteNomencladorResponse>
    {

        public DeleteNomencladorCommandHandler(IWorkContext workContext) : base(workContext)
        {
        }

        public override async Task<DeleteNomencladorResponse> Handle(DeleteNomencladorCommand command, CancellationToken cancellationToken)
        {
            var response = new DeleteNomencladorResponse();

            await em.DeleteAsync<Nomenclador>(command.Id);

            return response;
        }

       
    }
}

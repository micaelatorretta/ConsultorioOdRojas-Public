using AutoMapper;
using Domain.FunctionalUnits.EntidadDummies.Entities;
using Portable.FunctionalUnits.EntidadDummies.Commands;
using Portable.FunctionalUnits.EntidadDummies.DTOs;
using Portable.FunctionalUnits.EntidadDummies.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Application.Utils;

namespace Application.FunctionalUnits.EntidadDummies.Commands
{
    public class DeleteEntidadDummyDCommandHandler : BaseCommandHandler<DeleteEntidadDummyDCommand, DeleteEntidadDummyDResponse>
    {

        public DeleteEntidadDummyDCommandHandler(IWorkContext workContext) : base(workContext)
        {
        }

        public override async Task<DeleteEntidadDummyDResponse> Handle(DeleteEntidadDummyDCommand command, CancellationToken cancellationToken)
        {
            var response = new DeleteEntidadDummyDResponse();

            await em.DeleteAsync<EntidadDummyD>(command.Id);

            return response;
        }

       
    }
}

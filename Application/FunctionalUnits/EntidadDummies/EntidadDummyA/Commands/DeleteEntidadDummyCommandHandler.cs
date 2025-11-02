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
    public class DeleteEntidadDummyCommandHandler : BaseCommandHandler<DeleteEntidadDummyCommand, DeleteEntidadDummyResponse>
    {

        public DeleteEntidadDummyCommandHandler(IWorkContext workContext) : base(workContext)
        {
        }

        public override async Task<DeleteEntidadDummyResponse> Handle(DeleteEntidadDummyCommand command, CancellationToken cancellationToken)
        {
            var response = new DeleteEntidadDummyResponse();

            await em.DeleteAsync<EntidadDummy>(command.Id);

            return response;
        }

       
    }
}

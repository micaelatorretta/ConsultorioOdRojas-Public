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
    public class CreateEntidadDummyDCommandHandler : BaseCommandHandler<CreateEntidadDummyDCommand, CreateEntidadDummyDResponse>
    {
        private EntidadDummyD _entidadDummyD { get; set; } = null!;

        public CreateEntidadDummyDCommandHandler(IWorkContext workContext) : base(workContext)
        {
        }

        public override async Task<CreateEntidadDummyDResponse> Handle(CreateEntidadDummyDCommand command, CancellationToken cancellationToken)
        {
            var response = new CreateEntidadDummyDResponse();

            _entidadDummyD = WorkContext.Services.Mapper.Map<EntidadDummyD>(command.EntidadDummyD);
            await em.CreateAsync(_entidadDummyD);

            // Suscribirse a los eventos
            WorkContext.OnSuccess += HandleSuccess;

            return response;
        }

        /// <summary>
        /// Handler que maneja una respuesta satisfactoria.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleSuccess(object? sender, OnSuccessEventArgs<object> e)
        {
            var response = (e.Response as CreateEntidadDummyDResponse);

            // Por referencia se setea lo siguiente en la response:
            response!.EntidadDummyD = WorkContext.Services.Mapper.Map<EntidadDummyDDTO>(_entidadDummyD);

        }

    }
}

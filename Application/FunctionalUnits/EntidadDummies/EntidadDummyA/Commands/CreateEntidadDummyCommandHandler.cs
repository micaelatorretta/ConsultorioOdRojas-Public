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
    public class CreateEntidadDummyCommandHandler : BaseCommandHandler<CreateEntidadDummyCommand, CreateEntidadDummyResponse>
    {
        private EntidadDummy _entidadDummy { get; set; } = null!;

        public CreateEntidadDummyCommandHandler(IWorkContext workContext) : base(workContext)
        {
        }

        public override async Task<CreateEntidadDummyResponse> Handle(CreateEntidadDummyCommand command, CancellationToken cancellationToken)
        {
            var response = new CreateEntidadDummyResponse();

            _entidadDummy = WorkContext.Services.Mapper.Map<EntidadDummy>(command.EntidadDummy);
            await em.CreateAsync(_entidadDummy);

            // Suscribirse a los eventos
            WorkContext.OnSuccess += HandleSuccess;
            WorkContext.OnFailure += HandleFailure;

            return response;
        }

        /// <summary>
        /// Handler que maneja una respuesta satisfactoria.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleSuccess(object? sender, OnSuccessEventArgs<object> e)
        {
            var response = (e.Response as CreateEntidadDummyResponse);

            // Por referencia se setea lo siguiente en la response:
            response!.EntidadDummy = WorkContext.Services.Mapper.Map<EntidadDummyDTO>(_entidadDummy);

        }

        /// <summary>
        /// Handler para manejar valores en la response si hubo un error.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleFailure(object? sender, OnFailureEventArgs e)
        {
            // Lógica que quieres que se ejecute en caso de fallo
            Console.WriteLine($"Handler: Operación fallida con error: {e.Exception.Message}");
        }
    }
}

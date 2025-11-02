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
    public class UpdateEntidadDummyDCommandHandler : BaseCommandHandler<UpdateEntidadDummyDCommand, UpdateEntidadDummyDResponse>
    {
        private EntidadDummyD _entidadDummyD { get; set; } = null!;

        public UpdateEntidadDummyDCommandHandler(IWorkContext workContext) : base(workContext)
        {
        }


        public override async Task<UpdateEntidadDummyDResponse> Handle(UpdateEntidadDummyDCommand command, CancellationToken cancellationToken)
        {
            var response = new UpdateEntidadDummyDResponse();

            _entidadDummyD = WorkContext.Services.Mapper.Map<EntidadDummyD>(command.EntidadDummyD);

            // Update donde se tiene que personalizar el GetById en un repositorio especifico.
            // Para asi poder trackear las entidades
            await em.UpdateAsync(_entidadDummyD);

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
            var response = (e.Response as UpdateEntidadDummyDResponse);

            // Por referencia se setea lo siguiente en la response:
            response!.EntidadDummyD = WorkContext.Services.Mapper.Map<EntidadDummyDDTO>(_entidadDummyD);

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

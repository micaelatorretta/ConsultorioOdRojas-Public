using Domain.FunctionalUnits.ObrasSociales.Entities;
using Portable.FunctionalUnits.ObrasSociales.Commands;
using Portable.FunctionalUnits.ObrasSociales.DTOs;
using Portable.FunctionalUnits.ObrasSociales.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Application.Utils;
using Shared.Portable.Enums.EntityState;

namespace Application.FunctionalUnits.ObrasSociales.Commands
{
    public class UpdateObraSocialCommandHandler : BaseCommandHandler<UpdateObraSocialCommand, UpdateObraSocialResponse>
    {
        private ObraSocial _obraSocial { get; set; } = null!;

        public UpdateObraSocialCommandHandler(IWorkContext workContext) : base(workContext)
        {
        }


        public override async Task<UpdateObraSocialResponse> Handle(UpdateObraSocialCommand command, CancellationToken cancellationToken)
        {
            var response = new UpdateObraSocialResponse();

            _obraSocial = WorkContext.Services.Mapper.Map<ObraSocial>(command.ObraSocial);

            _obraSocial.EntityState = EntityStateMark.Modified;
            // Update donde se tiene que personalizar el GetById en un repositorio especifico.
            // Para asi poder trackear las entidades
            await em.UpdateAsync(_obraSocial);

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
            var response = (e.Response as UpdateObraSocialResponse);

            // Por referencia se setea lo siguiente en la response:
            response!.ObraSocial = WorkContext.Services.Mapper.Map<ObraSocialDTO>(_obraSocial);

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

using Domain.FunctionalUnits.ObrasSociales.Entities;
using Portable.FunctionalUnits.ObrasSociales.Commands;
using Portable.FunctionalUnits.ObrasSociales.DTOs;
using Portable.FunctionalUnits.ObrasSociales.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Application.Utils;

namespace Application.FunctionalUnits.ObrasSociales.Commands
{
    public class CreateObraSocialCommandHandler : BaseCommandHandler<CreateObraSocialCommand, CreateObraSocialResponse>
    {
        private ObraSocial _obraSocial { get; set; } = null!;

        public CreateObraSocialCommandHandler(IWorkContext workContext) : base(workContext)
        {
        }

        public override async Task<CreateObraSocialResponse> Handle(CreateObraSocialCommand command, CancellationToken cancellationToken)
        {
            var response = new CreateObraSocialResponse();

            _obraSocial = WorkContext.Services.Mapper.Map<ObraSocial>(command.ObraSocial);
            await em.CreateAsync(_obraSocial);

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
            var response = (e.Response as CreateObraSocialResponse);

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

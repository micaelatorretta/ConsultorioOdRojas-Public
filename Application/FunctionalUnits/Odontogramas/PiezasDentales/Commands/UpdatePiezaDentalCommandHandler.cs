using Domain.FunctionalUnits.Odontogramas.Entities;
using Portable.FunctionalUnits.Odontogramas.Commands;
using Portable.FunctionalUnits.Odontogramas.DTOs;
using Portable.FunctionalUnits.Odontogramas.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Application.Utils;

namespace Application.FunctionalUnits.Odontogramas.Commands
{
    public class UpdatePiezaDentalCommandHandler : BaseCommandHandler<UpdatePiezaDentalCommand, UpdatePiezaDentalResponse>
    {
        private PiezaDental _piezaDental { get; set; } = null!;

        public UpdatePiezaDentalCommandHandler(IWorkContext workContext) : base(workContext)
        {
        }


        public override async Task<UpdatePiezaDentalResponse> Handle(UpdatePiezaDentalCommand command, CancellationToken cancellationToken)
        {
            var response = new UpdatePiezaDentalResponse();

            _piezaDental = WorkContext.Services.Mapper.Map<PiezaDental>(command.PiezaDental);

            // Update donde se tiene que personalizar el GetById en un repositorio especifico.
            // Para asi poder trackear las entidades
            await em.UpdateAsync(_piezaDental);

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
            var response = e.Response as UpdatePiezaDentalResponse;

            // Por referencia se setea lo siguiente en la response:
            response!.PiezaDental = WorkContext.Services.Mapper.Map<PiezaDentalDTO>(_piezaDental);

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

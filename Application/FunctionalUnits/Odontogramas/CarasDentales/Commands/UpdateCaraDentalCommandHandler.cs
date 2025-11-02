using Domain.FunctionalUnits.Odontogramas.Entities;
using Portable.FunctionalUnits.Odontogramas.Commands;
using Portable.FunctionalUnits.Odontogramas.DTOs;
using Portable.FunctionalUnits.Odontogramas.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Application.Utils;

namespace Application.FunctionalUnits.Odontogramas.Commands
{
    public class UpdateCaraDentalCommandHandler : BaseCommandHandler<UpdateCaraDentalCommand, UpdateCaraDentalResponse>
    {
        private CaraDental _caraDental { get; set; } = null!;

        public UpdateCaraDentalCommandHandler(IWorkContext workContext) : base(workContext)
        {
        }


        public override async Task<UpdateCaraDentalResponse> Handle(UpdateCaraDentalCommand command, CancellationToken cancellationToken)
        {
            var response = new UpdateCaraDentalResponse();

            _caraDental = WorkContext.Services.Mapper.Map<CaraDental>(command.CaraDental);

            // Update donde se tiene que personalizar el GetById en un repositorio especifico.
            // Para asi poder trackear las entidades
            await em.UpdateAsync(_caraDental);

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
            var response = e.Response as UpdateCaraDentalResponse;

            // Por referencia se setea lo siguiente en la response:
            response!.CaraDental = WorkContext.Services.Mapper.Map<CaraDentalDTO>(_caraDental);

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

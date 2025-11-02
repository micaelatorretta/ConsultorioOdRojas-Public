using Domain.FunctionalUnits.Odontogramas.Entities;
using Portable.FunctionalUnits.Odontogramas.Commands;
using Portable.FunctionalUnits.Odontogramas.DTOs;
using Portable.FunctionalUnits.Odontogramas.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Application.Utils;
using Shared.Portable.Enums.EntityState;

namespace Application.FunctionalUnits.Odontogramas.Commands
{
    public class UpdateOdontogramaCommandHandler : BaseCommandHandler<UpdateOdontogramaCommand, UpdateOdontogramaResponse>
    {
        private Odontograma _Odontograma { get; set; } = null!;

        public UpdateOdontogramaCommandHandler(IWorkContext workContext) : base(workContext)
        {
        }


        public override async Task<UpdateOdontogramaResponse> Handle(UpdateOdontogramaCommand command, CancellationToken cancellationToken)
        {
            var response = new UpdateOdontogramaResponse();

            _Odontograma = WorkContext.Services.Mapper.Map<Odontograma>(command.Odontograma);

            _Odontograma.EntityState = EntityStateMark.Modified;
            // Update donde se tiene que personalizar el GetById en un repositorio especifico.
            // Para asi poder trackear las entidades
            await em.UpdateAsync(_Odontograma);

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
            var response = e.Response as UpdateOdontogramaResponse;

            // Por referencia se setea lo siguiente en la response:
            response!.Odontograma = WorkContext.Services.Mapper.Map<OdontogramaDTO>(_Odontograma);

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

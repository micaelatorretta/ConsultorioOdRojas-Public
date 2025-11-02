using Domain.FunctionalUnits.Odontogramas.Entities;
using Domain.FunctionalUnits.Odontogramas.Interfaces;
using Domain.FunctionalUnits.PiezasDentales.Interfaces;
using Domain.FunctionalUnits.Prestaciones.Entities;
using Portable.Enums;
using Portable.FunctionalUnits.Odontogramas.Commands;
using Portable.FunctionalUnits.Odontogramas.DTOs;
using Portable.FunctionalUnits.Odontogramas.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Application.Utils;
using Shared.Domain.Utils;
using Shared.Portable.Enums.EntityState;

namespace Application.FunctionalUnits.Odontogramas.Commands
{
    public class EliminarPrestacionOdontogramaCommandHandler : BaseCommandHandler<EliminarPrestacionOdontogramaCommand, EliminarPrestacionOdontogramaResponse>
    {
        private Odontograma _Odontograma { get; set; } = null!;

        public EliminarPrestacionOdontogramaCommandHandler(IWorkContext workContext) : base(workContext)
        {
        }

        public override async Task<EliminarPrestacionOdontogramaResponse> Handle(EliminarPrestacionOdontogramaCommand command, CancellationToken cancellationToken)
        {
            var response = new EliminarPrestacionOdontogramaResponse();

            var odontograma = await WorkContext.Services
                                               .ReadOnlyUnitOfWork
                                               .GetRepository<Odontograma, IOdontogramaRepository>()
                                               .GetByIdAsync(command.OdontogramaId);

            if (odontograma is null) return response;

            var tipoCara = TipoCaraExtensions.MapTipoCara(command.CaraDental);

            odontograma.EliminarPrestacion(command.NumeroPiezaDental, tipoCara);

            odontograma.EntityState = EntityStateMark.Modified;
            await em.UpdateAsync(odontograma);

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
            var response = e.Response as EliminarPrestacionOdontogramaResponse;

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

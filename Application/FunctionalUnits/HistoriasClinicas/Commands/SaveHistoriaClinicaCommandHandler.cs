using Application.FunctionalUnits.Auth.Commands.Strategies;
using Application.FunctionalUnits.HistoriasClinicas.Commands.Strategies;
using Domain.FunctionalUnits.HistoriasClinicas.Entities;
using Portable.FunctionalUnits.HistoriasClinicas.Commands;
using Portable.FunctionalUnits.HistoriasClinicas.DTOs;
using Portable.FunctionalUnits.HistoriasClinicas.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Application.Utils;

namespace Application.FunctionalUnits.HistoriasClinicas.Commands
{
    public class SaveHistoriaClinicaCommandHandler : BaseCommandHandler<SaveHistoriaClinicaCommand, SaveHistoriaClinicaResponse>
    {
        private HistoriaClinica _HistoriaClinica { get; set; } = null!;

        public SaveHistoriaClinicaCommandHandler(IWorkContext workContext) : base(workContext)
        {
        }

        public override async Task<SaveHistoriaClinicaResponse> Handle(SaveHistoriaClinicaCommand command, CancellationToken cancellationToken)
        {
            var response = new SaveHistoriaClinicaResponse();

            _HistoriaClinica = WorkContext.Services.Mapper.Map<HistoriaClinica>(command.HistoriaClinica);

            _HistoriaClinica = await em.RunAsync<HistoriaClinica>(new SaveHistoriaClinicaStrategy(WorkContext),
                                                                  new SaveHistoriaClinicaRecord(_HistoriaClinica));
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
            var response = (e.Response as SaveHistoriaClinicaResponse);

            // Por referencia se setea lo siguiente en la response:
            response!.HistoriaClinica = WorkContext.Services.Mapper.Map<HistoriaClinicaDTO>(_HistoriaClinica);

        }


    }
}

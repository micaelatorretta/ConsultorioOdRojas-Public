using Application.FunctionalUnits.Turnos.Commands.Strategies;
using Domain.FunctionalUnits.Turnos.Entities;
using Portable.FunctionalUnits.Turnos.Commands;
using Portable.FunctionalUnits.Turnos.DTOs;
using Portable.FunctionalUnits.Turnos.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Application.Utils;

namespace Application.FunctionalUnits.Turnos.Commands
{
    public class CreateTurnoCommandHandler : BaseCommandHandler<CreateTurnoCommand, CreateTurnoResponse>
    {
        private Turno _Turno { get; set; } = null!;

        public CreateTurnoCommandHandler(IWorkContext workContext) : base(workContext)
        {
        }

        public override async Task<CreateTurnoResponse> Handle(CreateTurnoCommand command, CancellationToken cancellationToken)
        {
            var response = new CreateTurnoResponse();

            _Turno = WorkContext.Services.Mapper.Map<Turno>(command.Turno);

            // Valida que no haya superposicion de turnos, en caso de haber lanza una excepcion.
            await em.RunAsync(new ExisteSuperposicionDeTurnosStrategy(WorkContext),
                              new ExisteSuperposicionDeTurnosRecord(_Turno));
           
            
            await em.CreateAsync(_Turno);

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
            var response = (e.Response as CreateTurnoResponse);

            // Por referencia se setea lo siguiente en la response:
            response!.Turno = WorkContext.Services.Mapper.Map<TurnoDTO>(_Turno);

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

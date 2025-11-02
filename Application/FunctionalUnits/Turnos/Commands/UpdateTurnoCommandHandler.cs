using Application.FunctionalUnits.Turnos.Commands.Strategies;
using Domain.FunctionalUnits.EntidadDummies.Entities;
using Domain.FunctionalUnits.Turnos.Entities;
using Portable.FunctionalUnits.EntidadDummies.Commands;
using Portable.FunctionalUnits.EntidadDummies.DTOs;
using Portable.FunctionalUnits.EntidadDummies.Responses;
using Portable.FunctionalUnits.Turnos.Commands;
using Portable.FunctionalUnits.Turnos.DTOs;
using Portable.FunctionalUnits.Turnos.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Application.Utils;
using Shared.Portable.Enums.EntityState;

namespace Application.FunctionalUnits.Turnos.Commands
{
    public class UpdateTurnoCommandHandler : BaseCommandHandler<UpdateTurnoCommand, UpdateTurnoResponse>
    {
        private Turno _turno { get; set; } = null!;

        public UpdateTurnoCommandHandler(IWorkContext workContext) : base(workContext)
        {
        }


        public override async Task<UpdateTurnoResponse> Handle(UpdateTurnoCommand command, CancellationToken cancellationToken)
        {
            var response = new UpdateTurnoResponse();

            _turno = WorkContext.Services.Mapper.Map<Turno>(command.Turno);

            // Valida que no haya superposicion de turnos, en caso de haber lanza una excepcion.
            await em.RunAsync(new ExisteSuperposicionDeTurnosStrategy(WorkContext),
                              new ExisteSuperposicionDeTurnosRecord(_turno));


            _turno.EntityState = EntityStateMark.Modified;
            // Update donde se tiene que personalizar el GetById en un repositorio especifico.
            // Para asi poder trackear las entidades
            await em.UpdateAsync(_turno);

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
            var response = (e.Response as UpdateTurnoResponse);

            // Por referencia se setea lo siguiente en la response:
            response!.Turno = WorkContext.Services.Mapper.Map<TurnoDTO>(_turno);

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

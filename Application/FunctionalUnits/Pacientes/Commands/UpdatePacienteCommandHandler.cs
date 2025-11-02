using Domain.FunctionalUnits.EntidadDummies.Entities;
using Domain.FunctionalUnits.HistoriasClinicas.Entities;
using Domain.FunctionalUnits.Pacientes.Entities;
using Portable.FunctionalUnits.EntidadDummies.Commands;
using Portable.FunctionalUnits.EntidadDummies.DTOs;
using Portable.FunctionalUnits.EntidadDummies.Responses;
using Portable.FunctionalUnits.Pacientes.Commands;
using Portable.FunctionalUnits.Pacientes.DTOs;
using Portable.FunctionalUnits.Pacientes.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Application.Utils;
using Shared.Portable.Enums.EntityState;

namespace Application.FunctionalUnits.Pacientes.Commands
{
    public class UpdatePacienteCommandHandler : BaseCommandHandler<UpdatePacienteCommand, UpdatePacienteResponse>
    {
        private Paciente _Paciente { get; set; } = null!;

        public UpdatePacienteCommandHandler(IWorkContext workContext) : base(workContext)
        {
        }


        public override async Task<UpdatePacienteResponse> Handle(UpdatePacienteCommand command, CancellationToken cancellationToken)
        {
            var response = new UpdatePacienteResponse();

            _Paciente = WorkContext.Services.Mapper.Map<Paciente>(command.Paciente);

            _Paciente.HistoriaClinica = null;
            _Paciente.EntityState = EntityStateMark.Modified;
            // Update donde se tiene que personalizar el GetById en un repositorio especifico.
            // Para asi poder trackear las entidades
            await em.UpdateAsync(_Paciente);

            await WorkContext.Services.UnitOfWork.CommitAsync(cancellationToken);

            var historiaClinica = WorkContext.Services.Mapper.Map<HistoriaClinica>(command.Paciente.HistoriaClinica);

            if (historiaClinica is not null)
            {
                historiaClinica.Paciente = _Paciente;
                historiaClinica.EntityState = EntityStateMark.Modified;
                // Update donde se tiene que personalizar el GetById en un repositorio especifico.
                // Para asi poder trackear las entidades
                await em.UpdateAsync(historiaClinica);

            }

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
            var response = (e.Response as UpdatePacienteResponse);

            // Por referencia se setea lo siguiente en la response:
            response!.Paciente = WorkContext.Services.Mapper.Map<PacienteDTO>(_Paciente);

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

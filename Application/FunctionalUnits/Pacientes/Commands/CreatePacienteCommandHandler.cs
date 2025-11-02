using Domain.FunctionalUnits.HistoriasClinicas.Entities;
using Domain.FunctionalUnits.Pacientes.Entities;
using Portable.FunctionalUnits.Pacientes.Commands;
using Portable.FunctionalUnits.Pacientes.DTOs;
using Portable.FunctionalUnits.Pacientes.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Application.Utils;

namespace Application.FunctionalUnits.Pacientes.Commands
{
    public class CreatePacienteCommandHandler : BaseCommandHandler<CreatePacienteCommand, CreatePacienteResponse>
    {
        private Paciente _Paciente { get; set; } = null!;

        public CreatePacienteCommandHandler(IWorkContext workContext) : base(workContext)
        {
        }

        public override async Task<CreatePacienteResponse> Handle(CreatePacienteCommand command, CancellationToken cancellationToken)
        {
            var response = new CreatePacienteResponse();

            _Paciente = WorkContext.Services.Mapper.Map<Paciente>(command.Paciente);
            var historiaClinica = WorkContext.Services.Mapper.Map<HistoriaClinica>(command.Paciente.HistoriaClinica);

            // se nullea la historia clinica porque no tiene id todavia
            _Paciente.HistoriaClinica = null;
            await em.CreateAsync(_Paciente);

            // Se commitea para obtener el id de paciente
            await WorkContext.Services.UnitOfWork.CommitAsync(cancellationToken);

            if (historiaClinica is not null)
            {
                // se asigna la referencia del paciente
                historiaClinica.Paciente = _Paciente;
                await em.CreateAsync(historiaClinica);

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
            var response = (e.Response as CreatePacienteResponse);

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

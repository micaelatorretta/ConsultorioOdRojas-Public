using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portable.FunctionalUnits.Pacientes.Commands;
using Portable.FunctionalUnits.Pacientes.Queries;
using Shared.Api.Base;

namespace Api.Controllers
{
    [Route("[controller]")]
    public class PacientesController : BaseApiController
    {
        public PacientesController(ILogger<PacientesController> logger, IMediator mediator) : base(logger, mediator)
        {
        }

        #region Paciente
        /// <summary>
        /// Caso de Uso: Creacion de un Paciente
        /// Envia la request al CommandHandler que defina el command: <see cref="CreatePacienteCommand"/>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> CreatePaciente([FromBody] CreatePacienteCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Caso de Uso: Actualiza un Paciente.
        /// Envia la request al CommandHandler que defina el command: <see cref="UpdatePacienteCommand"/>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> UpdatePaciente([FromBody] UpdatePacienteCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Caso de Uso: Borra un Paciente.
        /// Envia la request al CommandHandler que defina el command: <see cref="DeletePacienteCommand"/>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> DeletePaciente([FromBody] DeletePacienteCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Caso de Uso: Obtiene un Paciente por Id.
        /// Envia la request al QueryHandler que defina el command: <see cref="GetPacienteByIdQuery"/>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> GetPacienteById([FromBody] GetPacienteByIdQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Caso de Uso: Obtiene un listado de Paciente paginado.        
        /// Envia la request al QueryHandler que defina el command: <see cref="GetPacientePagedQuery"/>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> GetPacientePaged([FromBody] GetPacientePagedQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        #endregion

    }
}

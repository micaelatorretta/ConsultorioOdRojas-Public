using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portable.FunctionalUnits.Turnos.Commands;
using Portable.FunctionalUnits.Turnos.Queries;
using Shared.Api.Base;

namespace Api.Controllers
{
    [Route("[controller]")]
    public class TurnosController : BaseApiController
    {
        public TurnosController(ILogger<TurnosController> logger, IMediator mediator) : base(logger, mediator)
        {
        }

        #region Turno
        /// <summary>
        /// Caso de Uso: Creacion de una Turno
        /// Envia la request al CommandHandler que defina el command: <see cref="CreateTurnoCommand"/>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> CreateTurno([FromBody] CreateTurnoCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Caso de Uso: Actualiza una Turno.
        /// Envia la request al CommandHandler que defina el command: <see cref="UpdateTurnoCommand"/>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> UpdateTurno([FromBody] UpdateTurnoCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Caso de Uso: Borra una Turno.
        /// Envia la request al CommandHandler que defina el command: <see cref="DeleteTurnoCommand"/>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> DeleteTurno([FromBody] DeleteTurnoCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Caso de Uso: Obtiene una Turno por Id.
        /// Envia la request al QueryHandler que defina el command: <see cref="GetTurnoByIdQuery"/>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> GetTurnoById([FromBody] GetTurnoByIdQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Caso de Uso: Obtiene un listado de Turno paginado.        
        /// Envia la request al QueryHandler que defina el command: <see cref="GetTurnoPagedQuery"/>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> GetTurnoPaged([FromBody] GetTurnoPagedQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        #endregion

    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portable.FunctionalUnits.ObrasSociales.Commands;
using Portable.FunctionalUnits.ObrasSociales.Queries;
using Shared.Api.Base;

namespace Api.Controllers
{
    [Route("[controller]")]
    public class ObrasSocialesController : BaseApiController
    {
        public ObrasSocialesController(ILogger<ObrasSocialesController> logger, IMediator mediator) : base(logger, mediator)
        {
        }

        #region ObraSocial
        /// <summary>
        /// Caso de Uso: Creacion de una ObraSocial
        /// Envia la request al CommandHandler que defina el command: <see cref="CreateObraSocialCommand"/>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> CreateObraSocial([FromBody] CreateObraSocialCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Caso de Uso: Actualiza una ObraSocial.
        /// Envia la request al CommandHandler que defina el command: <see cref="UpdateObraSocialCommand"/>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> UpdateObraSocial([FromBody] UpdateObraSocialCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Caso de Uso: Borra una ObraSocial.
        /// Envia la request al CommandHandler que defina el command: <see cref="DeleteObraSocialCommand"/>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> DeleteObraSocial([FromBody] DeleteObraSocialCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Caso de Uso: Obtiene una ObraSocial por Id.
        /// Envia la request al QueryHandler que defina el command: <see cref="GetObraSocialByIdQuery"/>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> GetObraSocialById([FromBody] GetObraSocialByIdQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Caso de Uso: Obtiene un listado de ObraSocial paginado.        
        /// Envia la request al QueryHandler que defina el command: <see cref="GetObraSocialPagedQuery"/>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> GetObraSocialPaged([FromBody] GetObraSocialPagedQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        #endregion

    }
}

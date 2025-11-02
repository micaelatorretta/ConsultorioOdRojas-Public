using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portable.FunctionalUnits.EntidadDummies.Commands;
using Portable.FunctionalUnits.EntidadDummies.Queries;
using Shared.Api.Base;

namespace Api.Controllers
{
    [Route("[controller]")]
    public class EntidadDummiesController : BaseApiController
    {
        public EntidadDummiesController(ILogger<EntidadDummiesController> logger, IMediator mediator) : base(logger, mediator)
        {
        }

        #region EntidadDummyA
        /// <summary>
        /// Caso de Uso: Creacion de una EntidadDummy
        /// Envia la request al CommandHandler que defina el command: <see cref="CreateEntidadDummyCommand"/>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> CreateEntidadDummy([FromBody] CreateEntidadDummyCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Caso de Uso: Actualiza una EntidadDummy.
        /// Envia la request al CommandHandler que defina el command: <see cref="UpdateEntidadDummyCommand"/>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> UpdateEntidadDummy([FromBody] UpdateEntidadDummyCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Caso de Uso: Borra una EntidadDummy.
        /// Envia la request al CommandHandler que defina el command: <see cref="DeleteEntidadDummyCommand"/>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> DeleteEntidadDummy([FromBody] DeleteEntidadDummyCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Caso de Uso: Obtiene una EntidadDummy por Id.
        /// Envia la request al QueryHandler que defina el command: <see cref="GetEntidadDummyByIdQuery"/>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> GetEntidadDummyById([FromBody] GetEntidadDummyByIdQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Caso de Uso: Obtiene un listado de EntidadDummy paginado.        
        /// Envia la request al QueryHandler que defina el command: <see cref="GetEntidadDummyPagedQuery"/>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> GetEntidadDummyPaged([FromBody] GetEntidadDummyPagedQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        #endregion

        #region EntidadDummyD
        /// <summary>
        /// Caso de Uso: Creacion de una EntidadDummyD
        /// Envia la request al CommandHandler que defina el command: <see cref="CreateEntidadDummyDCommand"/>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> CreateEntidadDummyD([FromBody] CreateEntidadDummyDCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Caso de Uso: Actualiza una EntidadDummyD.
        /// Envia la request al CommandHandler que defina el command: <see cref="UpdateEntidadDummyDCommand"/>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> UpdateEntidadDummyD([FromBody] UpdateEntidadDummyDCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Caso de Uso: Borra una EntidadDummyD.
        /// Envia la request al CommandHandler que defina el command: <see cref="DeleteEntidadDummyDCommand"/>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> DeleteEntidadDummyD([FromBody] DeleteEntidadDummyDCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Caso de Uso: Obtiene una EntidadDummyD por Id.
        /// Envia la request al QueryHandler que defina el command: <see cref="GetEntidadDummyDByIdQuery"/>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> GetEntidadDummyDById([FromBody] GetEntidadDummyDByIdQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Caso de Uso: Obtiene un listado de EntidadDummyD paginado.        
        /// Envia la request al QueryHandler que defina el command: <see cref="GetEntidadDummyDPagedQuery"/>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> GetEntidadDummyDPaged([FromBody] GetEntidadDummyDPagedQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        #endregion
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portable.FunctionalUnits.Usuarios.Commands;
using Portable.FunctionalUnits.Usuarios.Queries;
using Shared.Api.Base;

namespace Api.Controllers
{
    [Route("[controller]")]
    public class UsuariosController : BaseApiController
    {
        public UsuariosController(ILogger<UsuariosController> logger, IMediator mediator) : base(logger, mediator)
        {
        }

        #region Usuario
        /// <summary>
        /// Caso de Uso: Creacion de una Usuario
        /// Envia la request al CommandHandler que defina el command: <see cref="CreateUsuarioCommand"/>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> CreateUsuario([FromBody] CreateUsuarioCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Caso de Uso: Actualiza una Usuario.
        /// Envia la request al CommandHandler que defina el command: <see cref="UpdateUsuarioCommand"/>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> UpdateUsuario([FromBody] UpdateUsuarioCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Caso de Uso: Borra una Usuario.
        /// Envia la request al CommandHandler que defina el command: <see cref="DeleteUsuarioCommand"/>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> DeleteUsuario([FromBody] DeleteUsuarioCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Caso de Uso: Obtiene una Usuario por Id.
        /// Envia la request al QueryHandler que defina el command: <see cref="GetUsuarioByIdQuery"/>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> GetUsuarioById([FromBody] GetUsuarioByIdQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Caso de Uso: Obtiene un listado de Usuario paginado.        
        /// Envia la request al QueryHandler que defina el command: <see cref="GetUsuarioPagedQuery"/>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> GetUsuarioPaged([FromBody] GetUsuarioPagedQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        #endregion

    }
}

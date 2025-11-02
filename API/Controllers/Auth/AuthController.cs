using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portable.FunctionalUnits.Auth.Commands;
using Portable.FunctionalUnits.ObrasSociales.Commands;
using Shared.Api.Base;

namespace Api.Controllers
{
    [Route("[controller]")]
    public class AuthController : BaseApiController
    {
        public AuthController(ILogger<AuthController> logger, IMediator mediator) : base(logger, mediator)
        {
        }

        /// <summary>
        /// Caso de Uso: Login de Usuario.
        /// Envia la request al CommandHandler que defina el command: <see cref="LoginCommand"/>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}

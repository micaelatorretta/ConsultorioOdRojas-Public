using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Portable.FunctionalUnits.RespaldosDeInformacion.Commands;
using Shared.Api.Base;

namespace Api.Controllers
{
    [Route("[controller]")]
    public class RespaldosDeInformacionController : BaseApiController
    {
        private readonly IWebHostEnvironment _env;
       // private readonly IConfiguration _configuration;
        public RespaldosDeInformacionController(ILogger<RespaldosDeInformacionController> logger,
            IMediator mediator, 
            IWebHostEnvironment env) : base(logger, mediator)
        {
            _env = env;
        //    _configuration = configuration;
        }

        /// <summary>
        /// Caso de Uso: Creacion de un Backup
        /// Envia la request al CommandHandler que defina el command: <see cref="CreateBackupCommand"/>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> CreateBackup([FromBody] CreateBackupCommand command)
        {
            // Establece los valores al command antes de viajar al handler.
            command.WebRootPath = _env.WebRootPath;
            command.Url = $"{HttpContext!.Request.Scheme}://{HttpContext.Request.Host}";
            
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("[Action]")]
        public async Task<IActionResult> RestoreDatabase([FromForm] RestoreDatabaseCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

    }
}

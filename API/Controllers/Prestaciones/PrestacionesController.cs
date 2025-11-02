using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portable.FunctionalUnits.Prestaciones.Commands;
using Portable.FunctionalUnits.Prestaciones.Queries;
using Shared.Api.Base;

namespace Api.Controllers
{
    [Route("[controller]")]
    public class PrestacionesController : BaseApiController
    {
        public PrestacionesController(ILogger<PrestacionesController> logger, IMediator mediator) : base(logger, mediator)
        {
        }

        #region Nomenclador
        /// <summary>
        /// Caso de Uso: Crea un Nomenclador.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> CreateNomenclador([FromBody] CreateNomencladorCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Caso de Uso: Actualiza un Nomenclador.
        /// Envia la request al CommandHandler que defina el command: <see cref="UpdateNomencladorCommand"/>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> UpdateNomenclador([FromBody] UpdateNomencladorCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Caso de Uso: Borra un Nomenclador.
        /// Envia la request al CommandHandler que defina el command: <see cref="DeleteNomencladorCommand"/>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> DeleteNomenclador([FromBody] DeleteNomencladorCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Caso de Uso: Obtiene un Nomenclador por Id.
        /// Envia la request al QueryHandler que defina el command: <see cref="GetNomencladorByIdQuery"/>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> GetNomencladorById([FromBody] GetNomencladorByIdQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Caso de Uso: Obtiene un listado de Nomenclador paginado.        
        /// Envia la request al QueryHandler que defina el command: <see cref="GetNomencladorPagedQuery"/>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> GetNomencladorPaged([FromBody] GetNomencladorPagedQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        #endregion

    }
}

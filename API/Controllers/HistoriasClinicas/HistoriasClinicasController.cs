using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portable.FunctionalUnits.HistoriasClinicas.Commands;
using Portable.FunctionalUnits.HistoriasClinicas.Queries;
using Shared.Api.Base;

namespace Api.Controllers
{
    [Route("[controller]")]
    public class HistoriasClinicasController : BaseApiController
    {
        public HistoriasClinicasController(ILogger<HistoriasClinicasController> logger, IMediator mediator) : base(logger, mediator)
        {
        }

        #region HistoriaClinica
        /// <summary>
        /// Caso de Uso: Creacion o actualizacion de una HistoriaClinica
        /// Envia la request al CommandHandler que defina el command: <see cref="SaveHistoriaClinicaCommand"/>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> SaveHistoriaClinica([FromBody] SaveHistoriaClinicaCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Caso de Uso: Obtiene una HistoriaClinica por Id.
        /// Envia la request al QueryHandler que defina el command: <see cref="GetHistoriaClinicaByIdQuery"/>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> GetHistoriaClinicaById([FromBody] GetHistoriaClinicaByIdQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Caso de Uso: Obtiene un listado de HistoriaClinica paginado.        
        /// Envia la request al QueryHandler que defina el command: <see cref="GetHistoriaClinicaPagedQuery"/>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> GetHistoriaClinicaPaged([FromBody] GetHistoriaClinicaPagedQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        #endregion

    }
}

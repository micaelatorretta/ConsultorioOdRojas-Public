using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portable.FunctionalUnits.Odontogramas.Commands;
using Portable.FunctionalUnits.Odontogramas.Queries;
using Shared.Api.Base;

namespace Api.Controllers
{
    [Route("[controller]")]
    public class OdontogramasController : BaseApiController
    {
        public OdontogramasController(ILogger<OdontogramasController> logger, IMediator mediator) : base(logger, mediator)
        {
        }


        #region Odontograma
        /// <summary>
        /// Caso de Uso: Aplicar Cara a Odontograma.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> AplicarCaraOdontograma([FromBody] AplicarCaraOdontogramaCommand command)
        {
            return Ok(await _mediator.Send(command));
        }      
        
        /// <summary>
        /// Caso de Uso: Eliminar prestación Odontograma.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> EliminarPrestacionOdontograma([FromBody] EliminarPrestacionOdontogramaCommand command)
        {
            return Ok(await _mediator.Send(command));
        }      
        
        /// <summary>
        /// Caso de Uso: Crea un Odontograma.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> CreateOdontograma([FromBody] CreateOdontogramaCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Caso de Uso: Actualiza un Odontograma.
        /// Envia la request al CommandHandler que defina el command: <see cref="UpdateOdontogramaCommand"/>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> UpdateOdontograma([FromBody] UpdateOdontogramaCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Caso de Uso: Borra un Odontograma.
        /// Envia la request al CommandHandler que defina el command: <see cref="DeleteOdontogramaCommand"/>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> DeleteOdontograma([FromBody] DeleteOdontogramaCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Caso de Uso: Obtiene un Odontograma por Id.
        /// Envia la request al QueryHandler que defina el command: <see cref="GetOdontogramaByIdQuery"/>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> GetOdontogramaById([FromBody] GetOdontogramaByIdQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Caso de Uso: Obtiene un listado de Odontograma paginado.        
        /// Envia la request al QueryHandler que defina el command: <see cref="GetOdontogramaPagedQuery"/>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> GetOdontogramaPaged([FromBody] GetOdontogramaPagedQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        #endregion


        #region Pieza Dental
        /// <summary>
        /// Caso de Uso: Crea una PiezaDental.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> CreatePiezaDental([FromBody] CreatePiezaDentalCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Caso de Uso: Actualiza una PiezaDental.
        /// Envia la request al CommandHandler que defina el command: <see cref="UpdatePiezaDentalCommand"/>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> UpdatePiezaDental([FromBody] UpdatePiezaDentalCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Caso de Uso: Borra una Pieza Dental.
        /// Envia la request al CommandHandler que defina el command: <see cref="DeletePiezaDentalCommand"/>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> DeletePiezaDental([FromBody] DeletePiezaDentalCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Caso de Uso: Obtiene una PiezaDental por Id.
        /// Envia la request al QueryHandler que defina el command: <see cref="GetPiezaDentalByIdQuery"/>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> GetPiezaDentalById([FromBody] GetPiezaDentalByIdQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Caso de Uso: Obtiene un listado de PiezaDental paginado.        
        /// Envia la request al QueryHandler que defina el command: <see cref="GetPiezaDentalPagedQuery"/>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> GetPiezaDentalPaged([FromBody] GetPiezaDentalPagedQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        #endregion

        #region Cara Dental
        /// <summary>
        /// Caso de Uso: Crea una CaraDental.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> CreateCaraDental([FromBody] CreateCaraDentalCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Caso de Uso: Actualiza una CaraDental.
        /// Envia la request al CommandHandler que defina el command: <see cref="UpdateCaraDentalCommand"/>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> UpdateCaraDental([FromBody] UpdateCaraDentalCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Caso de Uso: Borra una Pieza Dental.
        /// Envia la request al CommandHandler que defina el command: <see cref="DeleteCaraDentalCommand"/>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> DeleteCaraDental([FromBody] DeleteCaraDentalCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Caso de Uso: Obtiene una CaraDental por Id.
        /// Envia la request al QueryHandler que defina el command: <see cref="GetCaraDentalByIdQuery"/>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> GetCaraDentalById([FromBody] GetCaraDentalByIdQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Caso de Uso: Obtiene un listado de CaraDental paginado.        
        /// Envia la request al QueryHandler que defina el command: <see cref="GetCaraDentalPagedQuery"/>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> GetCaraDentalPaged([FromBody] GetCaraDentalPagedQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        #endregion

    }
}

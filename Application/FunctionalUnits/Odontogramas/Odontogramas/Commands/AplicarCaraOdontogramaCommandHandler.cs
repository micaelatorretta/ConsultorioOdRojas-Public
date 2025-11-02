using Domain.FunctionalUnits.Odontogramas.Entities;
using Domain.FunctionalUnits.Odontogramas.Interfaces;
using Domain.FunctionalUnits.PiezasDentales.Interfaces;
using Domain.FunctionalUnits.Prestaciones.Entities;
using Microsoft.EntityFrameworkCore;
using Portable.Enums;
using Portable.FunctionalUnits.Odontogramas.Commands;
using Portable.FunctionalUnits.Odontogramas.DTOs;
using Portable.FunctionalUnits.Odontogramas.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Application.Utils;
using Shared.Domain.Utils;
using Shared.Portable.Enums.EntityState;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.FunctionalUnits.Odontogramas.Commands
{
    public class AplicarCaraOdontogramaCommandHandler : BaseCommandHandler<AplicarCaraOdontogramaCommand, AplicarCaraOdontogramaResponse>
    {
        private Odontograma _Odontograma { get; set; } = null!;

        public AplicarCaraOdontogramaCommandHandler(IWorkContext workContext) : base(workContext)
        {
        }

        public override async Task<AplicarCaraOdontogramaResponse> Handle(AplicarCaraOdontogramaCommand command, CancellationToken cancellationToken)
        {
            var response = new AplicarCaraOdontogramaResponse();

            var odontograma = await WorkContext.Services
                                               .ReadOnlyUnitOfWork
                                               .GetRepository<Odontograma, IOdontogramaRepository>()
                                               .GetByIdAsync(command.OdontogramaId);

            if (odontograma is null) return response;

            var tipoCara = TipoCaraExtensions.MapTipoCara(command.CaraDental);

            var piezaDentalOdontograma = odontograma!.PiezasDentales.FirstOrDefault(p => p.PiezaDental.NumeroPieza == command.NumeroPiezaDental);

            var piezaDental = await WorkContext.Services
                                               .ReadOnlyUnitOfWork
                                               .GetRepository<PiezaDental, IPiezaDentalRepository>()
                                               .GetByNumero(command.NumeroPiezaDental);

           // var caraDentalOdontograma = await ConstruirCaraDental(piezaDental!, tipoCara, command.NomencladorId, command.ColorHexadecimal);

            var nomenclador = await WorkContext.Services
                                            .ReadOnlyUnitOfWork
                                            .GetRepository<Nomenclador>()
                                            .GetByIdAsync(GraphExplorerConfiguration.GetFull(), command.NomencladorId);

            //Caso 1: La pieza dental no existe en el odontograma
            //        1. Se agrega la pieza dental al odontograma
            //        2. Se agrega la cara dental a la pieza dental del odontograma
            if (piezaDentalOdontograma is null)
            {
                odontograma.AddPiezaDental(piezaDental!);
              
                odontograma.AplicarCaraDental(command.NumeroPiezaDental, tipoCara, nomenclador!, command.ColorHexadecimal);
            }
            // Caso 2: La pieza dental existe en el odontograma pero la cara dental se debe actualizar o agregar.
            else if (!piezaDentalOdontograma.CarasDentales.Any(cd => cd.CaraDental.CaraDentaria == tipoCara))
            {
                odontograma.AddCarasDentales(piezaDental!.CarasDentales, piezaDental.NumeroPieza);
                odontograma.AplicarCaraDental(command.NumeroPiezaDental, tipoCara, nomenclador!, command.ColorHexadecimal);
            }
            else if (piezaDentalOdontograma.CarasDentales.Any(cd => cd.CaraDental.CaraDentaria == tipoCara))
            {
                odontograma.AplicarCaraDental(command.NumeroPiezaDental, tipoCara, nomenclador!, command.ColorHexadecimal);
            }

            odontograma.EntityState = EntityStateMark.Modified;
            await em.UpdateAsync(odontograma);

            // Suscribirse a los eventos
            WorkContext.OnSuccess += HandleSuccess;
            WorkContext.OnFailure += HandleFailure;

            return response;
        }


        /// <summary>
        /// Handler que maneja una respuesta satisfactoria.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleSuccess(object? sender, OnSuccessEventArgs<object> e)
        {
            var response = e.Response as AplicarCaraOdontogramaResponse;

            // Por referencia se setea lo siguiente en la response:
            response!.Odontograma = WorkContext.Services.Mapper.Map<OdontogramaDTO>(_Odontograma);

        }

        /// <summary>
        /// Handler para manejar valores en la response si hubo un error.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleFailure(object? sender, OnFailureEventArgs e)
        {
            // Lógica que quieres que se ejecute en caso de fallo
            Console.WriteLine($"Handler: Operación fallida con error: {e.Exception.Message}");
        }
    }
}

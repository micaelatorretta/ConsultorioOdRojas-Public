using Domain.FunctionalUnits.Prestaciones.Entities;
using Portable.FunctionalUnits.Prestaciones.Commands;
using Portable.FunctionalUnits.Prestaciones.DTOs;
using Portable.FunctionalUnits.Prestaciones.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Application.Utils;

namespace Application.FunctionalUnits.Prestaciones.Commands
{
    public class CreateNomencladorCommandHandler : BaseCommandHandler<CreateNomencladorCommand, CreateNomencladorResponse>
    {
        private Nomenclador _Nomenclador { get; set; } = null!;

        public CreateNomencladorCommandHandler(IWorkContext workContext) : base(workContext)
        {
        }

        public override async Task<CreateNomencladorResponse> Handle(CreateNomencladorCommand command, CancellationToken cancellationToken)
        {
            var response = new CreateNomencladorResponse();

            _Nomenclador = WorkContext.Services.Mapper.Map<Nomenclador>(command.Nomenclador);
            await em.CreateAsync(_Nomenclador);

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
            var response = e.Response as CreateNomencladorResponse;

            // Por referencia se setea lo siguiente en la response:
            response!.Nomenclador = WorkContext.Services.Mapper.Map<NomencladorDTO>(_Nomenclador);

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

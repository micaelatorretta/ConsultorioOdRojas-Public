using Domain.FunctionalUnits.Odontogramas.Entities;
using Domain.FunctionalUnits.Odontogramas.Interfaces;
using Portable.FunctionalUnits.Odontogramas.DTOs;
using Portable.FunctionalUnits.Odontogramas.Queries;
using Portable.FunctionalUnits.Odontogramas.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Domain.Utils;

namespace Application.FunctionalUnits.Odontogramas.Queries
{
    public class GetOdontogramaByIdQueryHandler : BaseQueryHandler<GetOdontogramaByIdQuery, GetOdontogramaByIdResponse>
    {

        public GetOdontogramaByIdQueryHandler(IWorkContext workContext) : base(workContext) { }


        public override async Task<GetOdontogramaByIdResponse> Handle(GetOdontogramaByIdQuery query, CancellationToken cancellationToken)
        {
            var response = new GetOdontogramaByIdResponse();

            var graphFull = new GraphExplorerConfiguration() { Mode = GraphExplorerMode.Full };

            var odontograma = await WorkContext.Services.UnitOfWork.GetRepository<Odontograma, IOdontogramaRepository>()
                                                                   .GetByIdAsync(query.Id);

            if (odontograma is null) return response;

            // Agrega piezas y caras dentales faltantes al odontograma.
            odontograma = await AddPiezasOdontograma(odontograma);
            
            response.Odontograma = WorkContext.Services.Mapper.Map<OdontogramaDTO>(odontograma);
            return response;
        }

        private async Task<Odontograma> AddPiezasOdontograma(Odontograma odontograma)
        {
            var piezasDentales = await em.GetPagedAsync<PiezaDental>(new Shared.Domain.Pagination.PaginationConfiguration(new()
            {
                PageSize = Shared.Portable.Pagination.PaginationPageSize.All,
                PageNumber = 1,
            }));

            foreach (var piezaDental in piezasDentales)
            {
                // Si existe la pieza dental en el odontograma
                if (odontograma.PiezasDentales.Any(pd => pd.PiezaDental.NumeroPieza == piezaDental.NumeroPieza))
                {
                    odontograma.AddCarasDentales(piezaDental.CarasDentales, piezaDental.NumeroPieza);
                }
                // Si la pieza dental no existe en el odontograma
                else
                {
                    odontograma.AddPiezaDental(piezaDental);
                }
            }

            return odontograma;
        }
    }
}

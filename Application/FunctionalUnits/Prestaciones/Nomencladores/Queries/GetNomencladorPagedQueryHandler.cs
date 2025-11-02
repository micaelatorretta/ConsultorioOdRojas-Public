using Domain.FunctionalUnits.Prestaciones.Entities;
using Portable.FunctionalUnits.Prestaciones.DTOs;
using Portable.FunctionalUnits.Prestaciones.Queries;
using Portable.FunctionalUnits.Prestaciones.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Domain.Pagination;

namespace Application.FunctionalUnits.Prestaciones.Queries
{
    public class GetNomencladorPagedQueryHandler : BaseQueryHandler<GetNomencladorPagedQuery, GetNomencladorPagedResponse>
    {

        public GetNomencladorPagedQueryHandler(IWorkContext workContext) : base(workContext) { }


        public override async Task<GetNomencladorPagedResponse> Handle(GetNomencladorPagedQuery query, CancellationToken cancellationToken)
        {
            // Se puede personalizar el Grafo de exploración de entidades (tambien editar la profundidad de busqueda del mismo).
            var paginationConfiguration = new PaginationConfiguration(query.PaginationParams);

            // Construye la paginación en base a la configuración enviada por parametro.
            // Dentro de PaginationData tenemos los items, cantidad total de items en la bdd,
            // si tiene pagina siguiente, pagina anterior, y demas datos utiles utilizados para una paginación.
            var paginationData = await Paginate<Nomenclador, NomencladorDTO>(paginationConfiguration);

            var response = new GetNomencladorPagedResponse(paginationData);
            return response;
        }
    }
}

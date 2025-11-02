using Domain.FunctionalUnits.ObrasSociales.Entities;
using Portable.FunctionalUnits.ObrasSociales.DTOs;
using Portable.FunctionalUnits.ObrasSociales.Queries;
using Portable.FunctionalUnits.ObrasSociales.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Domain.Pagination;

namespace Application.FunctionalUnits.ObrasSociales.Queries
{
    public class GetObraSocialPagedQueryHandler : BaseQueryHandler<GetObraSocialPagedQuery, GetObraSocialPagedResponse>
    {

        public GetObraSocialPagedQueryHandler(IWorkContext workContext) : base(workContext) { }


        public override async Task<GetObraSocialPagedResponse> Handle(GetObraSocialPagedQuery query, CancellationToken cancellationToken)
        {
            // Se puede personalizar el Grafo de exploración de entidades (tambien editar la profundidad de busqueda del mismo).
            var paginationConfiguration = new PaginationConfiguration(query.PaginationParams);

            // Construye la paginación en base a la configuración enviada por parametro.
            // Dentro de PaginationData tenemos los items, cantidad total de items en la bdd,
            // si tiene pagina siguiente, pagina anterior, y demas datos utiles utilizados para una paginación.
            var paginationData = await Paginate<ObraSocial, ObraSocialDTO>(paginationConfiguration);

            var response = new GetObraSocialPagedResponse(paginationData);
            return response;
        }
    }
}

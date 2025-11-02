using Domain.FunctionalUnits.EntidadDummies.Entities;
using Portable.FunctionalUnits.EntidadDummies.DTOs;
using Portable.FunctionalUnits.EntidadDummies.Queries;
using Portable.FunctionalUnits.EntidadDummies.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Domain.Pagination;
using Shared.Domain.Utils;

namespace Application.FunctionalUnits.EntidadDummies.Queries
{
    public class GetEntidadDummyPagedQueryHandler : BaseQueryHandler<GetEntidadDummyPagedQuery, GetEntidadDummyPagedResponse>
    {

        public GetEntidadDummyPagedQueryHandler(IWorkContext workContext) : base(workContext) { }


        public override async Task<GetEntidadDummyPagedResponse> Handle(GetEntidadDummyPagedQuery query, CancellationToken cancellationToken)
        {
            // Se puede personalizar el Grafo de exploración de entidades (tambien editar la profundidad de busqueda del mismo).
            var paginationConfiguration = new PaginationConfiguration(query.PaginationParams);

            // Construye la paginación en base a la configuración enviada por parametro.
            // Dentro de PaginationData tenemos los items, cantidad total de items en la bdd,
            // si tiene pagina siguiente, pagina anterior, y demas datos utiles utilizados para una paginación.
            var paginationData = await Paginate<EntidadDummy, EntidadDummyDTO>(paginationConfiguration);

            var response = new GetEntidadDummyPagedResponse(paginationData);
            return response;
        }
    }
}

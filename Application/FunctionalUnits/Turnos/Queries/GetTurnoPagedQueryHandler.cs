using Domain.FunctionalUnits.Turnos.Entities;
using Portable.FunctionalUnits.Turnos.DTOs;
using Portable.FunctionalUnits.Turnos.Queries;
using Portable.FunctionalUnits.Turnos.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Domain.Pagination;

namespace Application.FunctionalUnits.Turnos.Queries
{
    public class GetTurnoPagedQueryHandler : BaseQueryHandler<GetTurnoPagedQuery, GetTurnoPagedResponse>
    {

        public GetTurnoPagedQueryHandler(IWorkContext workContext) : base(workContext) { }


        public override async Task<GetTurnoPagedResponse> Handle(GetTurnoPagedQuery query, CancellationToken cancellationToken)
        {
            // Se puede personalizar el Grafo de exploración de entidades (tambien editar la profundidad de busqueda del mismo).
            var paginationConfiguration = new PaginationConfiguration(query.PaginationParams);

            // Construye la paginación en base a la configuración enviada por parametro.
            // Dentro de PaginationData tenemos los items, cantidad total de items en la bdd,
            // si tiene pagina siguiente, pagina anterior, y demas datos utiles utilizados para una paginación.
            var paginationData = await Paginate<Turno, TurnoDTO>(paginationConfiguration);

            var response = new GetTurnoPagedResponse(paginationData);
            return response;
        }
    }
}

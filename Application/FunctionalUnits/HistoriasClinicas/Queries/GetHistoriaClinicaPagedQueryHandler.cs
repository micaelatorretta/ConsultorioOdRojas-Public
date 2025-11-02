using Domain.FunctionalUnits.HistoriasClinicas.Entities;
using Portable.FunctionalUnits.HistoriasClinicas.DTOs;
using Portable.FunctionalUnits.HistoriasClinicas.Queries;
using Portable.FunctionalUnits.HistoriasClinicas.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Domain.Pagination;

namespace Application.FunctionalUnits.HistoriasClinicas.Queries
{
    public class GetHistoriaClinicaPagedQueryHandler : BaseQueryHandler<GetHistoriaClinicaPagedQuery, GetHistoriaClinicaPagedResponse>
    {

        public GetHistoriaClinicaPagedQueryHandler(IWorkContext workContext) : base(workContext) { }


        public override async Task<GetHistoriaClinicaPagedResponse> Handle(GetHistoriaClinicaPagedQuery query, CancellationToken cancellationToken)
        {
            // Se puede personalizar el Grafo de exploración de entidades (tambien editar la profundidad de busqueda del mismo).
            var paginationConfiguration = new PaginationConfiguration(query.PaginationParams);

            // Construye la paginación en base a la configuración enviada por parametro.
            // Dentro de PaginationData tenemos los items, cantidad total de items en la bdd,
            // si tiene pagina siguiente, pagina anterior, y demas datos utiles utilizados para una paginación.
            var paginationData = await Paginate<HistoriaClinica, HistoriaClinicaDTO>(paginationConfiguration);

            var response = new GetHistoriaClinicaPagedResponse(paginationData);
            return response;
        }
    }
}

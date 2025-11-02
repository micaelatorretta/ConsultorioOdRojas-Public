using Domain.FunctionalUnits.Odontogramas.Entities;
using Portable.FunctionalUnits.Odontogramas.DTOs;
using Portable.FunctionalUnits.Odontogramas.Queries;
using Portable.FunctionalUnits.Odontogramas.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Domain.Pagination;

namespace Application.FunctionalUnits.Odontogramas.Queries
{
    public class GetCaraDentalPagedQueryHandler : BaseQueryHandler<GetCaraDentalPagedQuery, GetCaraDentalPagedResponse>
    {

        public GetCaraDentalPagedQueryHandler(IWorkContext workContext) : base(workContext) { }


        public override async Task<GetCaraDentalPagedResponse> Handle(GetCaraDentalPagedQuery query, CancellationToken cancellationToken)
        {
            // Se puede personalizar el Grafo de exploración de entidades (tambien editar la profundidad de busqueda del mismo).
            var paginationConfiguration = new PaginationConfiguration(query.PaginationParams);

            // Construye la paginación en base a la configuración enviada por parametro.
            // Dentro de PaginationData tenemos los items, cantidad total de items en la bdd,
            // si tiene pagina siguiente, pagina anterior, y demas datos utiles utilizados para una paginación.
            var paginationData = await Paginate<CaraDental, CaraDentalDTO>(paginationConfiguration);

            var response = new GetCaraDentalPagedResponse(paginationData);
            return response;
        }
    }
}

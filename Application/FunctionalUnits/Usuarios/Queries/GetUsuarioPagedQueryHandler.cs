using Domain.FunctionalUnits.Usuarios.Entities;
using Portable.FunctionalUnits.Usuarios.DTOs;
using Portable.FunctionalUnits.Usuarios.Queries;
using Portable.FunctionalUnits.Usuarios.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Domain.Pagination;

namespace Application.FunctionalUnits.Usuarios.Queries
{
    public class GetUsuarioPagedQueryHandler : BaseQueryHandler<GetUsuarioPagedQuery, GetUsuarioPagedResponse>
    {

        public GetUsuarioPagedQueryHandler(IWorkContext workContext) : base(workContext) { }


        public override async Task<GetUsuarioPagedResponse> Handle(GetUsuarioPagedQuery query, CancellationToken cancellationToken)
        {
            // Se puede personalizar el Grafo de exploración de entidades (tambien editar la profundidad de busqueda del mismo).
            var paginationConfiguration = new PaginationConfiguration(query.PaginationParams);

            // Construye la paginación en base a la configuración enviada por parametro.
            // Dentro de PaginationData tenemos los items, cantidad total de items en la bdd,
            // si tiene pagina siguiente, pagina anterior, y demas datos utiles utilizados para una paginación.
            var paginationData = await Paginate<Usuario, UsuarioDTO>(paginationConfiguration);

            var response = new GetUsuarioPagedResponse(paginationData);
            return response;
        }
    }
}

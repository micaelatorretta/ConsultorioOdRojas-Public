using Domain.FunctionalUnits.Pacientes.Entities;
using Portable.FunctionalUnits.Pacientes.DTOs;
using Portable.FunctionalUnits.Pacientes.Queries;
using Portable.FunctionalUnits.Pacientes.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Domain.Pagination;

namespace Application.FunctionalUnits.Pacientes.Queries
{
    public class GetPacientePagedQueryHandler : BaseQueryHandler<GetPacientePagedQuery, GetPacientePagedResponse>
    {

        public GetPacientePagedQueryHandler(IWorkContext workContext) : base(workContext) { }


        public override async Task<GetPacientePagedResponse> Handle(GetPacientePagedQuery query, CancellationToken cancellationToken)
        {
            // Se puede personalizar el Grafo de exploración de entidades (tambien editar la profundidad de busqueda del mismo).
            var paginationConfiguration = new PaginationConfiguration(query.PaginationParams);

            // Construye la paginación en base a la configuración enviada por parametro.
            // Dentro de PaginationData tenemos los items, cantidad total de items en la bdd,
            // si tiene pagina siguiente, pagina anterior, y demas datos utiles utilizados para una paginación.
            var paginationData = await Paginate<Paciente, PacienteDTO>(paginationConfiguration);

            var response = new GetPacientePagedResponse(paginationData);
            return response;
        }
    }
}

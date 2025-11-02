using Domain.FunctionalUnits.HistoriasClinicas.Entities;
using Domain.FunctionalUnits.HistoriasClinicas.Interfaces;
using Domain.FunctionalUnits.Odontogramas.Entities;
using Portable.FunctionalUnits.Odontogramas.DTOs;
using Portable.FunctionalUnits.Odontogramas.Queries;
using Portable.FunctionalUnits.Odontogramas.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Domain.Pagination;
using Shared.Portable.Pagination;

namespace Application.FunctionalUnits.Odontogramas.Queries
{
    public class GetOdontogramaPagedQueryHandler : BaseQueryHandler<GetOdontogramaPagedQuery, GetOdontogramaPagedResponse>
    {

        public GetOdontogramaPagedQueryHandler(IWorkContext workContext) : base(workContext) { }


        public override async Task<GetOdontogramaPagedResponse> Handle(GetOdontogramaPagedQuery query, CancellationToken cancellationToken)
        {
            // Se puede personalizar el Grafo de exploración de entidades (tambien editar la profundidad de busqueda del mismo).
            var paginationConfiguration = new PaginationConfiguration(query.PaginationParams);

            // Construye la paginación en base a la configuración enviada por parametro.
            // Dentro de PaginationData tenemos los items, cantidad total de items en la bdd,
            // si tiene pagina siguiente, pagina anterior, y demas datos utiles utilizados para una paginación.
            var paginationData = await Paginate<Odontograma, OdontogramaDTO>(paginationConfiguration);

            var response = new GetOdontogramaPagedResponse(paginationData);

            var filters = query.PaginationParams.Filters!.SelectMany(f => f.SubConditions).ToList();

            var pacienteIdValue = filters.SingleOrDefault(f => f.PropertyName == "Paciente.Id")!.Value!;
            int.TryParse(pacienteIdValue.ToString(), out int pacienteId);

            var historiaClinica = await WorkContext.Services
                                                    .ReadOnlyUnitOfWork
                                                    .GetRepository<HistoriaClinica, IHistoriaClinicaRepository>()
                                                    .GetByPacienteIdAsync(pacienteId);

            response.DetalleHistoriaClinica = historiaClinica?.ObtenerResumen() ?? string.Empty;
            return response;
        }
    }
}

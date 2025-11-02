using Domain.FunctionalUnits.Prestaciones.Entities;
using Portable.FunctionalUnits.Prestaciones.DTOs;
using Portable.FunctionalUnits.Prestaciones.Queries;
using Portable.FunctionalUnits.Prestaciones.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Domain.Utils;

namespace Application.FunctionalUnits.Prestaciones.Queries
{
    public class GetNomencladorByIdQueryHandler : BaseQueryHandler<GetNomencladorByIdQuery, GetNomencladorByIdResponse>
    {

        public GetNomencladorByIdQueryHandler(IWorkContext workContext) : base(workContext) { }


        public override async Task<GetNomencladorByIdResponse> Handle(GetNomencladorByIdQuery query, CancellationToken cancellationToken)
        {
            var response = new GetNomencladorByIdResponse();

            var graphFull = GraphExplorerConfiguration.GetFull();
            
            var nomenclador = await em.GetByIdAsync<Nomenclador>(graphFull, query.Id);

            response.Nomenclador = mapper.Map<NomencladorDTO>(nomenclador);
            return response;
        }
    }
}

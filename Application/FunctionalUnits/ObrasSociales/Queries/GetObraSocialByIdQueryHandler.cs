using Domain.FunctionalUnits.ObrasSociales.Entities;
using Portable.FunctionalUnits.ObrasSociales.DTOs;
using Portable.FunctionalUnits.ObrasSociales.Queries;
using Portable.FunctionalUnits.ObrasSociales.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Domain.Utils;

namespace Application.FunctionalUnits.ObrasSociales.Queries
{
    public class GetObraSocialByIdQueryHandler : BaseQueryHandler<GetObraSocialByIdQuery, GetObraSocialByIdResponse>
    {

        public GetObraSocialByIdQueryHandler(IWorkContext workContext) : base(workContext) { }


        public override async Task<GetObraSocialByIdResponse> Handle(GetObraSocialByIdQuery query, CancellationToken cancellationToken)
        {
            var response = new GetObraSocialByIdResponse();

            var graphFull = new GraphExplorerConfiguration() { Mode = GraphExplorerMode.Full };
            
            var obraSocial = await em.GetByIdAsync<ObraSocial>(graphFull, query.Id);

            response.ObraSocial = WorkContext.Services.Mapper.Map<ObraSocialDTO>(obraSocial);
            return response;
        }
    }
}

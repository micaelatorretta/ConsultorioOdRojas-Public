using Domain.FunctionalUnits.EntidadDummies.Entities;
using Portable.FunctionalUnits.EntidadDummies.DTOs;
using Portable.FunctionalUnits.EntidadDummies.Queries;
using Portable.FunctionalUnits.EntidadDummies.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Domain.Utils;

namespace Application.FunctionalUnits.EntidadDummies.Queries
{
    public class GetEntidadDummyDByIdQueryHandler : BaseQueryHandler<GetEntidadDummyDByIdQuery, GetEntidadDummyDByIdResponse>
    {

        public GetEntidadDummyDByIdQueryHandler(IWorkContext workContext) : base(workContext) { }


        public override async Task<GetEntidadDummyDByIdResponse> Handle(GetEntidadDummyDByIdQuery query, CancellationToken cancellationToken)
        {
            var response = new GetEntidadDummyDByIdResponse();

            var entidadDummyD = await em.GetByIdAsync<EntidadDummyD>(GraphExplorerConfiguration.GetFull(), query.Id);

            response.EntidadDummyD = WorkContext.Services.Mapper.Map<EntidadDummyDDTO>(entidadDummyD);
            return response;
        }
    }
}

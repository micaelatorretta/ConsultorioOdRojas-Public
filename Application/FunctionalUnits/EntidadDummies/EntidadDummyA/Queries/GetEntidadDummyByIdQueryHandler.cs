using Domain.FunctionalUnits.EntidadDummies.Entities;
using Portable.FunctionalUnits.EntidadDummies.DTOs;
using Portable.FunctionalUnits.EntidadDummies.Queries;
using Portable.FunctionalUnits.EntidadDummies.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Domain.Utils;

namespace Application.FunctionalUnits.EntidadDummies.Queries
{
    public class GetEntidadDummyByIdQueryHandler : BaseQueryHandler<GetEntidadDummyByIdQuery, GetEntidadDummyByIdResponse>
    {

        public GetEntidadDummyByIdQueryHandler(IWorkContext workContext) : base(workContext) { }


        public override async Task<GetEntidadDummyByIdResponse> Handle(GetEntidadDummyByIdQuery query, CancellationToken cancellationToken)
        {
            var response = new GetEntidadDummyByIdResponse();

            var graphFull = new GraphExplorerConfiguration() { Mode = GraphExplorerMode.Full };
            var graphFull2 = new GraphExplorerConfiguration() { Mode = GraphExplorerMode.Full };
            // Excluye la relacion 1 a 1 con EntidadDummyC porqur nombre de propiedad
            graphFull2.ExcludeProperty(nameof(EntidadDummy.DummyC));

            var graphFull3 = new GraphExplorerConfiguration() { Mode = GraphExplorerMode.Full };
            // Excluye la lista de List<EntidadDummyB> porque corresponde a este tipo 
            graphFull3.ExcludeType(typeof(List<EntidadDummyB>));

            var entidadDummy = await em.GetByIdAsync<EntidadDummy>(graphFull, query.Id);
            var entidadDummy2 = await em.GetByIdAsync<EntidadDummy>(graphFull2, query.Id);
            var entidadDummy3 = await em.GetByIdAsync<EntidadDummy>(graphFull3, query.Id);

            response.EntidadDummy = WorkContext.Services.Mapper.Map<EntidadDummyDTO>(entidadDummy);
            return response;
        }
    }
}

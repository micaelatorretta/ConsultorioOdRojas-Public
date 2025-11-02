using Domain.FunctionalUnits.Odontogramas.Entities;
using Portable.FunctionalUnits.Odontogramas.DTOs;
using Portable.FunctionalUnits.Odontogramas.Queries;
using Portable.FunctionalUnits.Odontogramas.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Domain.Utils;

namespace Application.FunctionalUnits.Odontogramas.Queries
{
    public class GetPiezaDentalByIdQueryHandler : BaseQueryHandler<GetPiezaDentalByIdQuery, GetPiezaDentalByIdResponse>
    {

        public GetPiezaDentalByIdQueryHandler(IWorkContext workContext) : base(workContext) { }


        public override async Task<GetPiezaDentalByIdResponse> Handle(GetPiezaDentalByIdQuery query, CancellationToken cancellationToken)
        {
            var response = new GetPiezaDentalByIdResponse();

            var graphFull = new GraphExplorerConfiguration() { Mode = GraphExplorerMode.Full };
            
            var piezaDental = await em.GetByIdAsync<PiezaDental>(graphFull, query.Id);

            response.PiezaDental = WorkContext.Services.Mapper.Map<PiezaDentalDTO>(piezaDental);
            return response;
        }
    }
}

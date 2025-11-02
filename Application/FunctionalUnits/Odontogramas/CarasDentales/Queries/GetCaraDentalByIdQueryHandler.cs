using Domain.FunctionalUnits.Odontogramas.Entities;
using Portable.FunctionalUnits.Odontogramas.DTOs;
using Portable.FunctionalUnits.Odontogramas.Queries;
using Portable.FunctionalUnits.Odontogramas.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Domain.Utils;

namespace Application.FunctionalUnits.Odontogramas.Queries
{
    public class GetCaraDentalByIdQueryHandler : BaseQueryHandler<GetCaraDentalByIdQuery, GetCaraDentalByIdResponse>
    {

        public GetCaraDentalByIdQueryHandler(IWorkContext workContext) : base(workContext) { }


        public override async Task<GetCaraDentalByIdResponse> Handle(GetCaraDentalByIdQuery query, CancellationToken cancellationToken)
        {
            var response = new GetCaraDentalByIdResponse();

            var graphFull = new GraphExplorerConfiguration() { Mode = GraphExplorerMode.Full };
            
            var caraDental = await em.GetByIdAsync<CaraDental>(graphFull, query.Id);

            response.CaraDental = WorkContext.Services.Mapper.Map<CaraDentalDTO>(caraDental);
            return response;
        }
    }
}

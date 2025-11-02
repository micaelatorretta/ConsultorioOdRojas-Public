using Domain.FunctionalUnits.HistoriasClinicas.Entities;
using Portable.FunctionalUnits.HistoriasClinicas.Queries;
using Portable.FunctionalUnits.HistoriasClinicas.DTOs;
using Portable.FunctionalUnits.HistoriasClinicas.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Domain.Utils;

namespace Application.FunctionalUnits.HistoriasClinicas.Queries
{
    public class GetHistoriaClinicaByIdQueryHandler : BaseQueryHandler<GetHistoriaClinicaByIdQuery, GetHistoriaClinicaByIdResponse>
    {
        
        public GetHistoriaClinicaByIdQueryHandler(IWorkContext workContext) : base(workContext) { }


        public override async Task<GetHistoriaClinicaByIdResponse> Handle(GetHistoriaClinicaByIdQuery query, CancellationToken cancellationToken)
        {
            var response = new GetHistoriaClinicaByIdResponse();

            var graphFull = new GraphExplorerConfiguration() { Mode = GraphExplorerMode.Full };
            
            var HistoriaClinica = await em.GetByIdAsync<HistoriaClinica>(graphFull, query.Id);

            response.HistoriaClinica = WorkContext.Services.Mapper.Map<HistoriaClinicaDTO>(HistoriaClinica);
            return response;
        }
    }
}

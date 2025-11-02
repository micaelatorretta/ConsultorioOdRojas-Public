using Portable.FunctionalUnits.HistoriasClinicas.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.HistoriasClinicas.Queries
{
    public class GetHistoriaClinicaByIdQuery : BaseQuery<GetHistoriaClinicaByIdResponse>
    {
        public GetHistoriaClinicaByIdQuery() { }
      
        public int Id { get; set; }
    }
}
using Portable.FunctionalUnits.Prestaciones.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Prestaciones.Queries
{
    public class GetNomencladorByIdQuery : BaseQuery<GetNomencladorByIdResponse>
    {
        public GetNomencladorByIdQuery() { }
      
        public int Id { get; set; }
    }
}
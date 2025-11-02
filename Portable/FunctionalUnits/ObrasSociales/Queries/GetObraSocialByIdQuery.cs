using Portable.FunctionalUnits.ObrasSociales.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.ObrasSociales.Queries
{
    public class GetObraSocialByIdQuery : BaseQuery<GetObraSocialByIdResponse>
    {
        public GetObraSocialByIdQuery() { }
      
        public int Id { get; set; }
    }
}
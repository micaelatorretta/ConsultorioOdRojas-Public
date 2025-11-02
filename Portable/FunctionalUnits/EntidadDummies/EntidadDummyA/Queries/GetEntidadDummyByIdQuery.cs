using Portable.FunctionalUnits.EntidadDummies.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.EntidadDummies.Queries
{
    public class GetEntidadDummyByIdQuery : BaseQuery<GetEntidadDummyByIdResponse>
    {
        public GetEntidadDummyByIdQuery() { }
      
        public int Id { get; set; }
    }
}
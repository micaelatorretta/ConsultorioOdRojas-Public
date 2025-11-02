using Portable.FunctionalUnits.EntidadDummies.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.EntidadDummies.Queries
{
    public class GetEntidadDummyDByIdQuery : BaseQuery<GetEntidadDummyDByIdResponse>
    {
        public GetEntidadDummyDByIdQuery() { }
      
        public int Id { get; set; }
    }
}
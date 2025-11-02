using Portable.FunctionalUnits.Odontogramas.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Odontogramas.Queries
{
    public class GetPiezaDentalByIdQuery : BaseQuery<GetPiezaDentalByIdResponse>
    {
        public GetPiezaDentalByIdQuery() { }
      
        public int Id { get; set; }
    }
}
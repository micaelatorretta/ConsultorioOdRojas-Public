using Portable.FunctionalUnits.Odontogramas.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Odontogramas.Queries
{
    public class GetCaraDentalByIdQuery : BaseQuery<GetCaraDentalByIdResponse>
    {
        public GetCaraDentalByIdQuery() { }
      
        public int Id { get; set; }
    }
}
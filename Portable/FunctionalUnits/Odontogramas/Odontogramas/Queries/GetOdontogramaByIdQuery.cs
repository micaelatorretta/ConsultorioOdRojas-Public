using Portable.FunctionalUnits.Odontogramas.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Odontogramas.Queries
{
    public class GetOdontogramaByIdQuery : BaseQuery<GetOdontogramaByIdResponse>
    {
        public GetOdontogramaByIdQuery() { }
      
        public int Id { get; set; }
    }
}
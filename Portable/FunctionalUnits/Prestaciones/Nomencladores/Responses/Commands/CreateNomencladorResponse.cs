using Portable.FunctionalUnits.Prestaciones.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.Prestaciones.Responses
{
    public class CreateNomencladorResponse : BaseResponse
    {
        public CreateNomencladorResponse()
        {

        }

        public NomencladorDTO? Nomenclador { get; set; } 
    }
}
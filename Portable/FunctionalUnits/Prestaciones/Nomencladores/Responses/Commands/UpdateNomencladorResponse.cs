using Portable.FunctionalUnits.Prestaciones.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.Prestaciones.Responses
{
    public class UpdateNomencladorResponse : BaseResponse
    {
        public UpdateNomencladorResponse()
        {

        }

        public NomencladorDTO? Nomenclador { get; set; } 
    }
}
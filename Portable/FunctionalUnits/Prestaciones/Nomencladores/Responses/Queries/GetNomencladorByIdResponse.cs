using Portable.FunctionalUnits.Odontogramas.DTOs;
using Portable.FunctionalUnits.Prestaciones.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.Prestaciones.Responses
{
    public class GetNomencladorByIdResponse : BaseResponse
    {
        public GetNomencladorByIdResponse() { }

        public NomencladorDTO Nomenclador { get; set; } = null!;
    }
}
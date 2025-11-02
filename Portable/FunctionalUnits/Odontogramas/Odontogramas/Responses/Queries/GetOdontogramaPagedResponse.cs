using Portable.FunctionalUnits.Odontogramas.DTOs;
using Shared.Portable.Base;
using Shared.Portable.Pagination;

namespace Portable.FunctionalUnits.Odontogramas.Responses
{
    public class GetOdontogramaPagedResponse : BasePagedResponse<OdontogramaDTO>
    {
        public GetOdontogramaPagedResponse() : base() { }
        public GetOdontogramaPagedResponse(PaginationData<OdontogramaDTO> paginationData)
            : base(paginationData) { }

        /// <summary>
        /// Detalle de la historia clinica del paciente.
        /// </summary>
        public string DetalleHistoriaClinica { get; set; } = string.Empty;
    }
}

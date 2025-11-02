using Portable.FunctionalUnits.HistoriasClinicas.DTOs;
using Portable.FunctionalUnits.HistoriasClinicas.Responses;
using Shared.Portable.Base;
using Shared.Portable.Pagination;

namespace Portable.FunctionalUnits.HistoriasClinicas.Queries
{
    /// <summary>
    /// Query paginada, para obtener todos los datos de HistoriaClinica.
    /// </summary>
    public class GetHistoriaClinicaPagedQuery : BasePagedQuery<HistoriaClinicaDTO, GetHistoriaClinicaPagedResponse>
    {
        public GetHistoriaClinicaPagedQuery(PaginationParams paginationParams) : base(paginationParams) { }
      
    }
}
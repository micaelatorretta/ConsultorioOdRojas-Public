using Domain.FunctionalUnits.HistoriasClinicas.Entities;
using Shared.Domain.Base.Interfaces;

namespace Domain.FunctionalUnits.HistoriasClinicas.Interfaces
{
    public interface IHistoriaClinicaRepository : IGenericRepository<HistoriaClinica>
    {
        public Task<HistoriaClinica?> GetByPacienteIdAsync(int pacienteId);
    }
}

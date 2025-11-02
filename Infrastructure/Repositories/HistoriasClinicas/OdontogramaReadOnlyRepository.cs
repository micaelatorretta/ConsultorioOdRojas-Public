using Domain.FunctionalUnits.HistoriasClinicas.Entities;
using Domain.FunctionalUnits.HistoriasClinicas.Interfaces;
using Infrastructure.Databases;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.HistoriasClinicas
{
    public class HistoriaClinicaReadOnlyRepository : GenericReadOnlyRepository<HistoriaClinica>, IHistoriaClinicaRepository
    {
        private readonly DbSet<HistoriaClinica> _HistoriaClinica;

        public HistoriaClinicaReadOnlyRepository(PrincipalDbContext context) : base(context)
        {
            _HistoriaClinica = context.Set<HistoriaClinica>();
        }

        public async Task<HistoriaClinica?> GetByPacienteIdAsync(int pacienteId)
        {
            return await _HistoriaClinica.Include(o => o.Paciente)
                                       .Where(o => o.Paciente.Id == pacienteId)
                                       .SingleOrDefaultAsync();
        }
    }
}

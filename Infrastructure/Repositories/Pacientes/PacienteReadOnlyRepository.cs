using Domain.FunctionalUnits.Odontogramas.Entities;
using Domain.FunctionalUnits.Odontogramas.Interfaces;
using Domain.FunctionalUnits.Pacientes.Entities;
using Domain.FunctionalUnits.Pacientes.Interfaces;
using Domain.FunctionalUnits.PiezasDentales.Interfaces;
using Infrastructure.Databases;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Pacientes
{
    public class PacienteReadOnlyRepository : GenericReadOnlyRepository<Paciente>, IPacienteRepository
    {
        private readonly DbSet<Paciente> _Paciente;

        public PacienteReadOnlyRepository(PrincipalDbContext context) : base(context)
        {
            _Paciente = context.Set<Paciente>();
        }

        public async Task<Paciente?> GetByIdAsync(int id)
        {
            return await _Paciente.Include(p => p.ObraSocial)
                                  .Include(p => p.HistoriaClinica)
                                  .SingleOrDefaultAsync(p => p.Id == id);
        }

    }
}

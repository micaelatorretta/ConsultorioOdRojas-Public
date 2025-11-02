using Domain.FunctionalUnits.Odontogramas.Entities;
using Domain.FunctionalUnits.Odontogramas.Interfaces;
using Domain.FunctionalUnits.Turnos.Entities;
using Domain.FunctionalUnits.Turnos.Interfaces;
using Domain.FunctionalUnits.PiezasDentales.Interfaces;
using Infrastructure.Databases;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Migrations;
using Domain.FunctionalUnits.Pacientes.Entities;

namespace Infrastructure.Repositories.Turnos
{
    public class TurnoReadOnlyRepository : GenericReadOnlyRepository<Turno>, ITurnoRepository
    {
        private readonly DbSet<Turno> _Turnos;

        public TurnoReadOnlyRepository(PrincipalDbContext context) : base(context)
        {
            _Turnos = context.Set<Turno>();
        }

        public Task<bool> ExisteSuperposicionAsync(Turno turno)
        {
            return _Turnos.Where(t => t.Id != turno.Id &&                 // excluir el propio (en edición)
                                      t.Fecha == turno.Fecha              // misma fecha
                                 )
                            // Regla de solapamiento: A.Inicio < B.Fin && B.Inicio < A.Fin
                            .AnyAsync(t => t.HoraInicio < turno.HoraFin && turno.HoraInicio < t.HoraFin);
        }

        public async Task<Turno?> GetByIdAsync(int id)
        {
            return await _Turnos.Include(p => p.Paciente)
                                .SingleOrDefaultAsync(p => p.Id == id);
        }
    }
}

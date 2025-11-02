using Domain.FunctionalUnits.Pacientes.Entities;
using Domain.FunctionalUnits.Turnos.Entities;
using Shared.Domain.Base.Interfaces;

namespace Domain.FunctionalUnits.Turnos.Interfaces
{
    public interface ITurnoRepository : IGenericRepository<Turno>
    {
        /// <summary>
        /// Devuelve true si existe una superposicion con el turno pasado por parametro
        /// </summary>
        /// <param name="turno"></param>
        /// <returns></returns>
        public Task<bool> ExisteSuperposicionAsync(Turno turno);

        /// <summary>
        /// GetById custom para agregar includes necesarios
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Turno?> GetByIdAsync(int id);
    }
}

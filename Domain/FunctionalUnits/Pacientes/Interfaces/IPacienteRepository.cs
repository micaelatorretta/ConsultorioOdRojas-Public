using Domain.FunctionalUnits.Odontogramas.Entities;
using Domain.FunctionalUnits.Pacientes.Entities;
using Domain.FunctionalUnits.Usuarios.Entities;
using Shared.Domain.Base.Interfaces;

namespace Domain.FunctionalUnits.Pacientes.Interfaces
{
    public interface IPacienteRepository : IGenericRepository<Paciente>
    {
        /// <summary>
        /// GetById custom para agregar includes necesarios
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Paciente?> GetByIdAsync(int id);
    }
}

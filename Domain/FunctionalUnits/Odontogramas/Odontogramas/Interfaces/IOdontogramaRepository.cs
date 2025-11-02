using Domain.FunctionalUnits.Odontogramas.Entities;
using Domain.FunctionalUnits.Usuarios.Entities;
using Shared.Domain.Base.Interfaces;

namespace Domain.FunctionalUnits.Odontogramas.Interfaces
{
    public interface IOdontogramaRepository : IGenericRepository<Odontograma>
    {
        public Task<Odontograma?> GetByIdAsync(int id);
    }
}

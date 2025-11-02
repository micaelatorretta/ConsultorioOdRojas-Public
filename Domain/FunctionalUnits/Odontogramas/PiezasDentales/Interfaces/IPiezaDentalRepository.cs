using Domain.FunctionalUnits.Odontogramas.Entities;
using Domain.FunctionalUnits.Usuarios.Entities;
using Shared.Domain.Base.Interfaces;

namespace Domain.FunctionalUnits.PiezasDentales.Interfaces
{
    public interface IPiezaDentalRepository : IGenericRepository<PiezaDental>
    {
        public Task<PiezaDental?> GetByNumero(byte numeroPieza);
    }
}

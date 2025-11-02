using Domain.FunctionalUnits.Odontogramas.Entities;
using Domain.FunctionalUnits.Odontogramas.Interfaces;
using Domain.FunctionalUnits.PiezasDentales.Interfaces;
using Infrastructure.Databases;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.PiezasDentales
{
    public class PiezaDentalReadOnlyRepository : GenericReadOnlyRepository<PiezaDental>, IPiezaDentalRepository
    {
        private readonly DbSet<PiezaDental> _PiezaDental;

        public PiezaDentalReadOnlyRepository(PrincipalDbContext context) : base(context)
        {
            _PiezaDental = context.Set<PiezaDental>();
        }

        public async Task<PiezaDental?> GetByNumero(byte numeroPieza)
        {
            return await _PiezaDental.Include(p => p.CarasDentales)
                                     .SingleOrDefaultAsync(p => p.NumeroPieza == numeroPieza);
        }
    }
}

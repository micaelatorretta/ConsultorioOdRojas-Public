using Domain.FunctionalUnits.Odontogramas.Entities;
using Domain.FunctionalUnits.Odontogramas.Interfaces;
using Domain.FunctionalUnits.Usuarios.Entities;
using Infrastructure.Databases;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Base.Interfaces;
using Shared.Domain.Pagination;
using Shared.Domain.Utils;

namespace Infrastructure.Repositories.Odontogramas
{
    public class OdontogramaReadOnlyRepository : GenericReadOnlyRepository<Odontograma>, IOdontogramaRepository
    {
        private readonly DbSet<Odontograma> _odontograma;

        public OdontogramaReadOnlyRepository(PrincipalDbContext context) : base(context)
        {
            _odontograma = context.Set<Odontograma>();
        }

        public async Task<Odontograma?> GetByIdAsync(int id)
        {
           return await _odontograma.Include(o => o.Paciente)
                                    .Include(o => o.PiezasDentales)
                                         .ThenInclude(o => o.CarasDentales)
                                             .ThenInclude(c => c.CaraDental)
                                     .Include(o => o.PiezasDentales)
                                         .ThenInclude(o => o.CarasDentales)
                                             .ThenInclude(c => c.Nomenclador)
                                                 .ThenInclude(c => c.ObraSocial)
                                    .Include(o => o.PiezasDentales)
                                        .ThenInclude(o => o.PiezaDental)
                                    .Where(o => o.Id == id)
                                    .SingleOrDefaultAsync();
        }
    }
}

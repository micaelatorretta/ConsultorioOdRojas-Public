using Domain.FunctionalUnits.EntidadDummies.Entities;
using Domain.FunctionalUnits.EntidadDummies.Interfaces;
using Infrastructure.Databases;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.EntidadDummies
{
    public class EntidadDummyRepository : GenericRepository<EntidadDummy>, IEntidadDummyRepository
    {
        //private readonly PrincipalDbContext _context;
        private readonly DbSet<EntidadDummy> _entidadDummy;

        public EntidadDummyRepository(PrincipalDbContext context) : base(context)
        {
            _entidadDummy = context.Set<EntidadDummy>();
        }

        //public async Task<IEnumerable<EntidadDummy>> GetByCustomCriteriaAsync(string criteria)
        //{
        //    return await _context.EntidadDummies
        //                         .Where(e => e.Criteria == criteria)
        //                         .ToListAsync();
        //}
    }

}

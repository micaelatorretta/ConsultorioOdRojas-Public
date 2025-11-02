using Domain.FunctionalUnits.Auth.Interfaces;
using Domain.FunctionalUnits.Usuarios.Entities;
using Infrastructure.Databases;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Auth
{
    public class AuthReadOnlyRepository : GenericReadOnlyRepository<Usuario>, IAuthRepository
    {
        private readonly DbSet<Usuario> _usuario;

        public AuthReadOnlyRepository(PrincipalDbContext context) : base(context)
        {
            _usuario = context.Set<Usuario>();
        }

      
        public async Task<Usuario?> LoginAsync(string username)
        {
            return await _usuario.Where(u => u.Username == username)
                                 .SingleOrDefaultAsync();
        }

        
    }

}

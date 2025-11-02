using Domain.FunctionalUnits.Usuarios.Entities;
using Shared.Domain.Base.Interfaces;

namespace Domain.FunctionalUnits.Auth.Interfaces
{
    public interface IAuthRepository : IGenericRepository<Usuario>
    {
        public Task<Usuario?> LoginAsync(string username);
    }
}

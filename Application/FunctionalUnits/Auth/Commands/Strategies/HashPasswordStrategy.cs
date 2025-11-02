using Shared.Application.Services.Interfaces;
using Shared.Application.Strategies;
using System.Security.Cryptography;

namespace Application.FunctionalUnits.Auth.Commands.Strategies
{
    /// <summary>
    /// Record para recibir los datos en la estrategia de hash de contraseña.
    /// </summary>
    /// <param name="Password"></param>
    public record HashPasswordRecord(string Password);

    /// <summary>
    /// Estrategia para hashear una contraseña.
    /// </summary>
    public class HashPasswordStrategy : BehaviorAdapterStrategy
    {
        public HashPasswordStrategy(IWorkContext workContext) : base(workContext)
        {
        }

        public override async Task<TResult> RunAsync<TResult>(object[] args)
        {
            if(typeof(TResult) != typeof((string, string)))
            {
                throw new ArgumentException("El resultado de la estrategia debe ser una tupla de (string, string).");
            }

            var record = ValidateArgs<HashPasswordRecord>(args).Single(); // Validar que solo se recibe un argumento.

            if (string.IsNullOrEmpty(record.Password))
            {
                (string, string) tupla = new(string.Empty, string.Empty);
                return (TResult)(object)tupla;
            }
            var result = HashPassword(record.Password);

            await Task.CompletedTask;
            return (TResult)(object)result;
        }

        private (string Hash, string Salt) HashPassword(string password)
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] salt = new byte[16];
            rng.GetBytes(salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32);

            return (Convert.ToBase64String(hash), Convert.ToBase64String(salt));
        }
    }
}

using Shared.Application.Services.Interfaces;
using Shared.Application.Strategies;
using System.Security.Cryptography;

namespace Application.FunctionalUnits.Auth.Commands.Strategies
{
    /// <summary>
    /// Record para recibir los datos en la estrategia de verificacion de contraseña.
    /// </summary>
    /// <param name="Password"></param>
    public record VerifyPasswordRecord(string enteredPassword, string storedHash, string storedSalt);

    /// <summary>
    /// Estrategia para verificar una contraseña.
    /// </summary>
    public class VerifyPasswordStrategy : BehaviorAdapterStrategy
    {
        public VerifyPasswordStrategy(IWorkContext workContext) : base(workContext)
        {
        }

        public override async Task<TResult> RunAsync<TResult>(object[] args)
        {
            if(typeof(TResult) != typeof(bool))
            {
                throw new ArgumentException("El resultado de la estrategia debe ser un booleano.");
            }

            var record = ValidateArgs<VerifyPasswordRecord>(args).Single(); // Validar que solo se recibe un argumento.

            var isValid = VerifyPassword(record.enteredPassword, record.storedHash, record.storedSalt);

            await Task.CompletedTask;
            return (TResult)(object)isValid;
        }

        private bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            byte[] saltBytes = Convert.FromBase64String(storedSalt);

            using var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 100000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32);

            return Convert.ToBase64String(hash) == storedHash;
        }

    }
}

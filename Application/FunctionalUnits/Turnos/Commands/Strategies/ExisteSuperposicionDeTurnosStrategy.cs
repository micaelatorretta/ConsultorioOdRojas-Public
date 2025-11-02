using Domain.FunctionalUnits.Turnos.Entities;
using Domain.FunctionalUnits.Turnos.Interfaces;
using Shared.Application.Services.Interfaces;
using Shared.Application.Strategies;
using System.Security.Cryptography;

namespace Application.FunctionalUnits.Turnos.Commands.Strategies
{
    public record ExisteSuperposicionDeTurnosRecord(Turno turno);

    /// <summary>
    /// Estrategia para verificar una contraseña.
    /// </summary>
    public class ExisteSuperposicionDeTurnosStrategy : BehaviorAdapterStrategy
    {
        public ExisteSuperposicionDeTurnosStrategy(IWorkContext workContext) : base(workContext)
        {
        }

        public override async Task RunAsync(object[] args)
        {
            //if(typeof(TResult) != typeof(bool))
            //{
            //    throw new ArgumentException("El resultado de la estrategia debe ser un booleano.");
            //}

            var record = ValidateArgs<ExisteSuperposicionDeTurnosRecord>(args).Single(); // Validar que solo se recibe un argumento.

            var existeSuperposicion = await WorkContext.Services
                                                       .ReadOnlyUnitOfWork
                                                       .GetRepository<Turno, ITurnoRepository>()
                                                       .ExisteSuperposicionAsync(record.turno);


            if (existeSuperposicion) throw new Exception("Existe superposición entre turnos, verifique que la fecha y los horarios de los turnos existentes.");
           // return (TResult)(object)existeSuperposicion;
        }

       
    }
}

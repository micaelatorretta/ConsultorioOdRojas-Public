using Domain.FunctionalUnits.HistoriasClinicas.Entities;
using Shared.Application.Services.Interfaces;
using Shared.Application.Strategies;
using Shared.Portable.Enums.EntityState;
using System.Security.Cryptography;

namespace Application.FunctionalUnits.HistoriasClinicas.Commands.Strategies
{
    public record SaveHistoriaClinicaRecord(HistoriaClinica HistoriaClinica);

    /// <summary>
    /// Estrategia para crear o actualizar una historia clinica.
    /// </summary>
    public class SaveHistoriaClinicaStrategy : BehaviorAdapterStrategy
    {
        private IEntityManager em => WorkContext.Services.EntityManager;
        public SaveHistoriaClinicaStrategy(IWorkContext workContext) : base(workContext)
        {
        }

        public override async Task<TResult> RunAsync<TResult>(object[] args)
        {
            if(typeof(TResult) != typeof(HistoriaClinica))
            {
                throw new ArgumentException("El resultado de la estrategia debe ser del tipo HistoriaClinica.");
            }

            var record = ValidateArgs<SaveHistoriaClinicaRecord>(args).Single(); // Validar que solo se recibe un argumento.

            var historiaClinica = record.HistoriaClinica;

            // Si es nueva se crea
            if (historiaClinica.IsNew())
            {
                await em.CreateAsync(historiaClinica);

            }
            // Si no es nueva se actualiza
            else
            {
                historiaClinica.EntityState = EntityStateMark.Modified;
                await em.UpdateAsync(historiaClinica);
            }

            return (TResult)(object)historiaClinica;
        }

      
    }
}

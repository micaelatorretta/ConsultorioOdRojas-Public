using Domain.FunctionalUnits.Auth.Interfaces;
using Domain.FunctionalUnits.EntidadDummies.Interfaces;
using Domain.FunctionalUnits.HistoriasClinicas.Interfaces;
using Domain.FunctionalUnits.Odontogramas.Interfaces;
using Domain.FunctionalUnits.Pacientes.Interfaces;
using Domain.FunctionalUnits.PiezasDentales.Interfaces;
using Domain.FunctionalUnits.Turnos.Interfaces;
using Infrastructure.Databases;
using Infrastructure.Repositories.Auth;
using Infrastructure.Repositories.EntidadDummies;
using Infrastructure.Repositories.HistoriasClinicas;
using Infrastructure.Repositories.Odontogramas;
using Infrastructure.Repositories.Pacientes;
using Infrastructure.Repositories.PiezasDentales;
using Infrastructure.Repositories.Turnos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Databases;
using Shared.Infrastructure.DependencyInjection;

namespace Infrastructure.DependencyInjection
{
    public static class InfrastructureDependencyInjection
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, string connectionString)
        {
            // Inyecta los servicios de Shared.Infrastructure
            services.AddSharedInfrastructureLayer();

            services.AddDbContext<BaseDbContext, PrincipalDbContext>(
                            options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Infrastructure"))
                        );


            #region Repositories
            services.AddTransient<IEntidadDummyRepository, EntidadDummyRepository>();

            #region Auth
            services.AddTransient<IAuthRepository, AuthReadOnlyRepository>();
            #endregion
            
            #region Odontograma
            services.AddTransient<IOdontogramaRepository, OdontogramaReadOnlyRepository>();
            #endregion

            #region PiezaDental
            services.AddTransient<IPiezaDentalRepository, PiezaDentalReadOnlyRepository>();
            #endregion

            #region HistoriasClinicas
            services.AddTransient<IHistoriaClinicaRepository, HistoriaClinicaReadOnlyRepository>();
            #endregion
            
            #region Pacientes
            services.AddTransient<IPacienteRepository, PacienteReadOnlyRepository>();
            #endregion

            #region Turnos
            services.AddTransient<ITurnoRepository, TurnoReadOnlyRepository>();
            #endregion

            #endregion


        }

    }
}

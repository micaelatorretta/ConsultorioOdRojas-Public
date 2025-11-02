using Domain.FunctionalUnits.EntidadDummies.Entities;
using Domain.FunctionalUnits.HistoriasClinicas.Entities;
using Domain.FunctionalUnits.ObrasSociales.Entities;
using Domain.FunctionalUnits.Odontogramas.Entities;
using Domain.FunctionalUnits.Pacientes.Entities;
using Domain.FunctionalUnits.Prestaciones.Entities;
using Domain.FunctionalUnits.Turnos.Entities;
using Domain.FunctionalUnits.Usuarios.Entities;
using Infrastructure.Databases.EntityConfigurations;
using Infrastructure.Databases.EntityConfigurations.Pacientes;
using Infrastructure.Databases.EntityConfigurations.Usuarios;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Databases;
using Shared.Infrastructure.Extensions;

namespace Infrastructure.Databases
{
    public class PrincipalDbContext : BaseDbContext
    {
        #region DbSets

        #region Turnos
        public DbSet<Turno> Turno { get; set; }
        #endregion
        
        #region Obras Sociales
        public DbSet<ObraSocial> ObraSocial { get; set; }
        #endregion

        #region Usuarios
        public DbSet<Usuario> Usuario { get; set; }

        #endregion
        #region Pacientes
        public DbSet<Paciente> Paciente { get; set; }
        #endregion

        #region Odontrogramas

        #region Odontrograma
        public DbSet<Odontograma> Odontograma { get; set; }
        public DbSet<OdontogramaPiezaDental> OdontogramaPiezaDental { get; set; }
        public DbSet<OdontogramaCaraDental> OdontogramaCaraDental { get; set; }
        #endregion

        #region PiezaDental
        public DbSet<PiezaDental> PiezaDental { get; set; }
        #endregion

        #region Cara Dental
        public DbSet<CaraDental> CaraDental { get; set; }
        #endregion

        #endregion

        #region Prestaciones
        public DbSet<Nomenclador> Nomenclador { get; set; }
        #endregion

        #region HistoriasClinicas
        public DbSet<HistoriaClinica> HistoriaClinica { get; set; }
        #endregion

        #region EntidadDummies

        #region DummyA
        public DbSet<EntidadDummy> EntidadDummy { get; set; }
        public DbSet<EntidadDummyB> EntidadDummyB { get; set; }
        public DbSet<EntidadDummyC> EntidadDummyC { get; set; }

        #endregion

        #region DummyD
        public DbSet<EntidadDummyD> EntidadDummyD { get; set; }
        #endregion

        #endregion

        #endregion

        public PrincipalDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Entity Configurations

            #region Turnos
            modelBuilder.ApplyConfiguration(new TurnoEntityConfiguration());
            #endregion

            #region ObrasSociales
            modelBuilder.ApplyConfiguration(new ObraSocialEntityConfiguration());
            #endregion

            #region Usuarios
            modelBuilder.ApplyConfiguration(new UsuarioEntityConfiguration());
            #endregion

            #region Pacientes
            modelBuilder.ApplyConfiguration(new PacienteEntityConfiguration());
            #endregion

            #region Odontogramas

            #region Odontograma
            modelBuilder.ApplyConfiguration(new OdontogramaEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OdontogramaPiezaDentalEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OdontogramaCaraDentalEntityConfiguration());
            #endregion

            modelBuilder.ApplyConfiguration(new PiezaDentalEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CaraDentalEntityConfiguration());
            #endregion

            #region Prestaciones
            modelBuilder.ApplyConfiguration(new NomencladorEntityConfiguration());
            #endregion

            #region HistoriasClinicas
            modelBuilder.ApplyConfiguration(new HistoriaClinicaEntityConfiguration());
            #endregion

            #region EntidadDummies

            #region EntidadDummyA
            modelBuilder.ApplyConfiguration(new EntidadDummyEntityConfiguration());
            modelBuilder.ApplyConfiguration(new EntidadDummyBEntityConfiguration());
            modelBuilder.ApplyConfiguration(new EntidadDummyCEntityConfiguration());
            #endregion

            #region EntidadDummyD
            modelBuilder.ApplyConfiguration(new EntidadDummyDEntityConfiguration());
            #endregion

            #endregion

            #endregion

            modelBuilder.ExcludeDeletedAggregates();

            base.OnModelCreating(modelBuilder);
        
        }

       

    }
}
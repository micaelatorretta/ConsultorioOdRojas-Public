using Domain.FunctionalUnits.HistoriasClinicas.Entities;
using Domain.FunctionalUnits.ObrasSociales.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.EntityConfigurations
{
    public class HistoriaClinicaEntityConfiguration : IEntityTypeConfiguration<HistoriaClinica>
    {
        public void Configure(EntityTypeBuilder<HistoriaClinica> builder)
        {
            // Especificar la tabla asociada (opcional si coincide con el nombre de la clase)
            builder.ToTable(nameof(HistoriaClinica));

            // Configurar la clave primaria
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Observaciones).HasMaxLength(200);

            builder.HasOne(hc => hc.Paciente)
                   .WithOne(p => p.HistoriaClinica)
                   .HasForeignKey<HistoriaClinica>(hc => hc.PacienteId)
                   .IsRequired();

            #region ValueObjects
            builder.OwnsOne(e => e.IntervencionesUlt2Anios, voBuilder =>
            {
                voBuilder.Property(ct => ct.Detalle).HasMaxLength(250);
            });
            
            builder.OwnsOne(e => e.TratamientoMedico, voBuilder =>
            {
                voBuilder.Property(ct => ct.Detalle).HasMaxLength(250);
            });
            
            builder.OwnsOne(e => e.Alergias, voBuilder =>
            {
                voBuilder.Property(ct => ct.Detalle).HasMaxLength(250);
            });

            builder.OwnsOne(e => e.AnestesiasPrevias, voBuilder =>
            {
                voBuilder.Property(ct => ct.Detalle).HasMaxLength(250);
            });

            builder.OwnsOne(e => e.SangradoExcesivo, voBuilder =>
            {
                voBuilder.Property(ct => ct.Detalle).HasMaxLength(250);
            });

            builder.OwnsOne(e => e.MedicacionActual, voBuilder =>
            {
                voBuilder.Property(ct => ct.Detalle).HasMaxLength(250);
            });

            builder.OwnsOne(e => e.Radiografias);

            builder.OwnsOne(e => e.Fuma);

            builder.OwnsOne(e => e.Bebe);


            #endregion
        }
    }
}

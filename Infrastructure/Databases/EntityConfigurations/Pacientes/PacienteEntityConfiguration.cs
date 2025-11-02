using Domain.FunctionalUnits.Pacientes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.EntityConfigurations.Pacientes
{
    public class PacienteEntityConfiguration : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            // Especificar la tabla asociada (opcional si coincide con el nombre de la clase)
            builder.ToTable(nameof(Paciente));
            // Configurar la clave primaria
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Nombre).HasMaxLength(50);
            builder.Property(e => e.Apellido).HasMaxLength(50);
            builder.Property(e => e.DNI).HasMaxLength(8);
            builder.HasOne(p => p.ObraSocial)
                   .WithMany()
                   .HasForeignKey(p => p.ObraSocialId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

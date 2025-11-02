using Domain.FunctionalUnits.Turnos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.EntityConfigurations
{
    public class TurnoEntityConfiguration : IEntityTypeConfiguration<Turno>
    {
        public void Configure(EntityTypeBuilder<Turno> builder)
        {
            // Especificar la tabla asociada (opcional si coincide con el nombre de la clase)
            builder.ToTable(nameof(Turno));

            // Configurar la clave primaria
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Descripcion).HasMaxLength(200);

            builder.HasOne(t => t.Paciente)
                    .WithMany()
                    .HasForeignKey(t => t.PacienteId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Navigation(t => t.Paciente).AutoInclude();
        }
    }
}

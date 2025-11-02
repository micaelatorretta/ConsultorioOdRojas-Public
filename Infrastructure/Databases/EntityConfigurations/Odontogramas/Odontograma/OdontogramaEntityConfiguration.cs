using Domain.FunctionalUnits.Odontogramas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.EntityConfigurations
{
    public class OdontogramaEntityConfiguration : IEntityTypeConfiguration<Odontograma>
    {
        public void Configure(EntityTypeBuilder<Odontograma> builder)
        {
            // Especificar la tabla asociada (opcional si coincide con el nombre de la clase)
            builder.ToTable(nameof(Odontograma));

            // Configurar la clave primaria
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.HasOne(o => o.Paciente)
                   .WithMany(p => p.Odontogramas)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

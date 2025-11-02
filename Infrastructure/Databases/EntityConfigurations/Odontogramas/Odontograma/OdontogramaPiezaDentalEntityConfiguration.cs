using Domain.FunctionalUnits.Odontogramas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.EntityConfigurations
{
    public class OdontogramaPiezaDentalEntityConfiguration : IEntityTypeConfiguration<OdontogramaPiezaDental>
    {
        public void Configure(EntityTypeBuilder<OdontogramaPiezaDental> builder)
        {
            // Especificar la tabla asociada (opcional si coincide con el nombre de la clase)
            builder.ToTable(nameof(OdontogramaPiezaDental));

            // Configurar la clave primaria
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.HasOne(opd => opd.PiezaDental)
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(opd => opd.Odontograma)
                   .WithMany(o => o.PiezasDentales)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

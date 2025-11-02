using Domain.FunctionalUnits.Odontogramas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.EntityConfigurations
{
    public class OdontogramaCaraDentalEntityConfiguration : IEntityTypeConfiguration<OdontogramaCaraDental>
    {
        public void Configure(EntityTypeBuilder<OdontogramaCaraDental> builder)
        {
            // Especificar la tabla asociada (opcional si coincide con el nombre de la clase)
            builder.ToTable(nameof(OdontogramaCaraDental));

            // Configurar la clave primaria
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.HasOne(opd => opd.CaraDental)
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(opd => opd.ObraSocial)
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(opd => opd.Nomenclador)
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(opd => opd.PiezaDental)
                   .WithMany(o => o.CarasDentales)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

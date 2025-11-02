using Domain.FunctionalUnits.ObrasSociales.Entities;
using Domain.FunctionalUnits.Prestaciones.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.EntityConfigurations
{
    public class NomencladorEntityConfiguration : IEntityTypeConfiguration<Nomenclador>
    {
        public void Configure(EntityTypeBuilder<Nomenclador> builder)
        {
            // Especificar la tabla asociada (opcional si coincide con el nombre de la clase)
            builder.ToTable(nameof(Nomenclador));

            // Configurar la clave primaria
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.CodigoNacional).HasMaxLength(30);
            builder.Property(e => e.Descripcion).HasMaxLength(150);
            builder.Property(e => e.Importe).HasPrecision(13,2);
            builder.HasOne(n => n.ObraSocial)
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

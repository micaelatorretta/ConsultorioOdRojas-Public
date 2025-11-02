using Domain.FunctionalUnits.Odontogramas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.EntityConfigurations
{
    public class PiezaDentalEntityConfiguration : IEntityTypeConfiguration<PiezaDental>
    {
        public void Configure(EntityTypeBuilder<PiezaDental> builder)
        {
            // Especificar la tabla asociada (opcional si coincide con el nombre de la clase)
            builder.ToTable(nameof(PiezaDental));

            // Configurar la clave primaria
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.HasIndex(e => e.NumeroPieza)
                .IsUnique()
                .HasDatabaseName("IX_PiezaDental_NumeroPieza");
    }
    }
}

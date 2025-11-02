using Domain.FunctionalUnits.EntidadDummies.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.EntityConfigurations
{
    public class EntidadDummyCEntityConfiguration : IEntityTypeConfiguration<EntidadDummyC>
    {
        public void Configure(EntityTypeBuilder<EntidadDummyC> builder)
        {
            // Especificar la tabla asociada (opcional si coincide con el nombre de la clase)
            builder.ToTable(nameof(EntidadDummyC));

            // Configurar la clave primaria
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name).HasMaxLength(50);


        }
    }
}

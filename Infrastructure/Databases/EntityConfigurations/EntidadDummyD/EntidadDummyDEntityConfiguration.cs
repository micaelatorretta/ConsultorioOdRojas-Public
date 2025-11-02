using Domain.FunctionalUnits.EntidadDummies.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.EntityConfigurations
{
    public class EntidadDummyDEntityConfiguration : IEntityTypeConfiguration<EntidadDummyD>
    {
        public void Configure(EntityTypeBuilder<EntidadDummyD> builder)
        {
            // Especificar la tabla asociada (opcional si coincide con el nombre de la clase)
            builder.ToTable(nameof(EntidadDummyD));

            // Configurar la clave primaria
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name).HasMaxLength(50);

            builder.HasMany(e => e.DummiesA)
                .WithMany(ea => ea.DummiesD)
                // Configuracion de la tabla intermedia de relacion N -> N entre DummyA y DummyD
                .UsingEntity(c => 
                {
                    c.ToTable($"{nameof(EntidadDummyD)}{nameof(EntidadDummy)}");
                    // Se pueden agregar mas configuraciones.
                });
        }
    }

}

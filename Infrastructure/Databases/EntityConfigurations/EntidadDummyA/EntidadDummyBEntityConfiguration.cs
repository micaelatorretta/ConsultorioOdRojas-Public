using Domain.FunctionalUnits.EntidadDummies.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.EntityConfigurations
{
    public class EntidadDummyBEntityConfiguration : IEntityTypeConfiguration<EntidadDummyB>
    {
        public void Configure(EntityTypeBuilder<EntidadDummyB> builder)
        {
            // Especificar la tabla asociada (opcional si coincide con el nombre de la clase)
            builder.ToTable(nameof(EntidadDummyB));

            // Configurar la clave primaria
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name).HasMaxLength(50);

            builder.HasOne(eb => eb.EntidadDummy)
                   .WithMany(e => e.DummiesB)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

        }
    }
}

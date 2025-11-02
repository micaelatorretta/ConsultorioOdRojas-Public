using Domain.FunctionalUnits.EntidadDummies.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.EntityConfigurations
{
    public class EntidadDummyEntityConfiguration : IEntityTypeConfiguration<EntidadDummy>
    {
        public void Configure(EntityTypeBuilder<EntidadDummy> builder)
        {
            // Especificar la tabla asociada (opcional si coincide con el nombre de la clase)
            builder.ToTable(nameof(EntidadDummy));

            // Configurar la clave primaria
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name).HasMaxLength(50);

            builder.HasOne(e => e.DummyC)
                .WithOne(ec => ec.EntidadDummy)
                // Primer argumento: Tipo de la Entidad dependiente.
                // Segundo argumento: Nombre de la FK.
                .HasForeignKey(typeof(EntidadDummyC), $"{nameof(EntidadDummy)}{nameof(EntidadDummy.Id)}")
                .IsRequired();

            #region ValueObjects

            // Configuracion del ValueObject de ContactoTelefonico.
            // Las propiedades de ContactoTelefonico se van a agregar a la tabla de EntidadDummy como:
            // Contacto_NumeroTelefono, Contacto_TipoTelefono ....
            builder.OwnsOne(e => e.Contacto, contactoTelefonicoBuilder => 
            {
                // Configuro la propiedad de tipo String del VO de ContactoTelefonico.
                contactoTelefonicoBuilder.Property(ct => ct.NumeroTelefono).HasMaxLength(50);
            });
            #endregion
        }
    }

}

using Domain.FunctionalUnits.Usuarios.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.EntityConfigurations.Usuarios
{
    public class UsuarioEntityConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            // Especificar la tabla asociada (opcional si coincide con el nombre de la clase)
            builder.ToTable(nameof(Usuario));
            // Configurar la clave primaria
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Nombre).HasMaxLength(50);
            builder.Property(e => e.Apellido).HasMaxLength(50);
            builder.Property(e => e.Email).HasMaxLength(60);

            builder.HasIndex(e => e.Username).IsUnique();
        }
    }
}

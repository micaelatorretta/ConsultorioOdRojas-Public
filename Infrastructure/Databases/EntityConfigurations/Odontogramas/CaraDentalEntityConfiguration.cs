using Domain.FunctionalUnits.Odontogramas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.EntityConfigurations
{
    public class CaraDentalEntityConfiguration : IEntityTypeConfiguration<CaraDental>
    {
        public void Configure(EntityTypeBuilder<CaraDental> builder)
        {
            // Especificar la tabla asociada (opcional si coincide con el nombre de la clase)
            builder.ToTable(nameof(CaraDental));

            // Configurar la clave primaria
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            
            builder.HasOne(cd => cd.PiezaDental)
                   .WithMany(pd => pd.CarasDentales)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

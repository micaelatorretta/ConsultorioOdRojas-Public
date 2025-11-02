using Domain.FunctionalUnits.Odontogramas.Rules;
using Shared.Domain.Base;
using Shared.Domain.Extensions;

namespace Domain.FunctionalUnits.Odontogramas.Entities
{
    public partial class OdontogramaPiezaDental : BaseEntity
    {
        public OdontogramaPiezaDental() { }
        public PiezaDental PiezaDental { get; set; } = null!;
        public Odontograma Odontograma { get; set; } = null!;
        /// <summary>
        /// Caras dentales en las que se realizó una prestación
        /// </summary>
        public List<OdontogramaCaraDental> CarasDentales { get; set; } = new();
    }

}
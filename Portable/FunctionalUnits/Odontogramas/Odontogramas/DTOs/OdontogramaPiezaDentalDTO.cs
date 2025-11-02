using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Odontogramas.DTOs
{
    public class OdontogramaPiezaDentalDTO : BaseDTO
    {
        public OdontogramaPiezaDentalDTO() { }
        public PiezaDentalDTO PiezaDental { get; set; } = null!;
        public OdontogramaDTO Odontograma { get; set; } = null!;
        /// <summary>
        /// Caras dentales en las que se realizó una prestación
        /// </summary>
        public List<OdontogramaCaraDentalDTO> CarasDentales { get; set; } = new();
    }
}

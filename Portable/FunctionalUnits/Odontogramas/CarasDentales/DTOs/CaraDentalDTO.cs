using Portable.Enums;
using Portable.ValueObjectsDTO;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Odontogramas.DTOs
{
    public class CaraDentalDTO : BaseDTO
    {
        public CaraDentalDTO() { }
        public TipoCara CaraDentaria { get; set; }
        public PiezaDentalDTO PiezaDental { get; set; } = null!;
    }
}

using Portable.Enums;
using Portable.ValueObjectsDTO;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Odontogramas.DTOs
{
    public class PiezaDentalDTO : BaseDTO
    {
        public PiezaDentalDTO() { }

        public byte NumeroPieza { get; set; }
        public TipoCuadrante Cuadrante { get; set; }
        public bool DenticionPermanente { get; set; }
        public List<CaraDentalDTO> CarasDentales { get; set; } = new();


    }
}

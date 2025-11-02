using Portable.ValueObjectsDTO;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.EntidadDummies.DTOs
{
    public class EntidadDummyDTO : BaseDTO
    {
        public EntidadDummyDTO() { }

        public string Name { get; set; } = string.Empty;
        public EntidadDummyCDTO DummyC { get; set; } = null!;
        // ValueObject
        public ContactoTelefonicoDTO? Contacto { get; set; }
        public List<EntidadDummyBDTO> DummiesB { get; set; } = new();

        public List<EntidadDummyDDTO> DummiesD { get; set; } = new();

    }
}

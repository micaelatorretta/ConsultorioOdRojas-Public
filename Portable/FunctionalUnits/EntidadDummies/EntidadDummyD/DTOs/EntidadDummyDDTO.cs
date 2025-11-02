using Portable.ValueObjectsDTO;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.EntidadDummies.DTOs
{
    public class EntidadDummyDDTO : BaseDTO
    {
        public EntidadDummyDDTO() { }

        public string Name { get; set; } = string.Empty;
       
        public List<EntidadDummyDTO> DummiesA { get; set; } = new();
    }
}

using Shared.Portable.Base;

namespace Portable.FunctionalUnits.EntidadDummies.DTOs
{
    public class EntidadDummyCDTO : BaseDTO
    {
        public EntidadDummyCDTO() { }

        public string Name { get; set; } = string.Empty;
        public EntidadDummyDTO? EntidadDummy { get; set; } 
    }
}

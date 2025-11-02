using Shared.Portable.Base;

namespace Portable.FunctionalUnits.EntidadDummies.DTOs
{
    public class EntidadDummyBDTO : BaseDTO
    {
        public EntidadDummyBDTO() { }

        public string Name { get; set; } = string.Empty;
        public EntidadDummyDTO? EntidadDummy { get; set; } 
    }
}

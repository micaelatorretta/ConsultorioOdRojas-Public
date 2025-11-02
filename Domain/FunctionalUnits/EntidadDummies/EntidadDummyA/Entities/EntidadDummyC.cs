using Shared.Domain.Base;

namespace Domain.FunctionalUnits.EntidadDummies.Entities
{
    /// <summary>
    ///  Clase que NO es Aggregate
    /// </summary>
    public class EntidadDummyC : BaseEntity
    {
        public EntidadDummyC() { }

        public string Name { get; set; } = string.Empty;
        // Relacion 1 a 1
        public EntidadDummy EntidadDummy { get; set; } = null!;
    }
}

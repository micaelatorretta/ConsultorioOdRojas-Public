using Shared.Domain.Base;

namespace Domain.FunctionalUnits.EntidadDummies.Entities
{
    /// <summary>
    /// EntidadDummy para probar la relacion de 1 -> N
    /// NO es Aggregate
    /// </summary>
    public class EntidadDummyB : BaseEntity
    {
        public EntidadDummyB() { }

        public string Name { get; set; } = string.Empty;
        public EntidadDummy EntidadDummy { get; set; } = null!;
    }
}

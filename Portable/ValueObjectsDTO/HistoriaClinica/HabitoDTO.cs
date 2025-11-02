using Shared.Portable.Base;

// Hábitos con cantidad (p. ej. cigarrillos por día / vasos por semana)
namespace Portable.ValueObjects
{
    public class HabitoDTO : BaseValueObjectDTO
    {
        private HabitoDTO()
        {
        }
        public HabitoDTO(bool tiene, int? cantidad)
        {
            Tiene = tiene;
            Cantidad = tiene ? cantidad : null;
        }

        public bool Tiene { get; set;  }
        public int? Cantidad { get; set; }

        public static HabitoDTO No() => new(false, null);

        public static HabitoDTO Si(int? cantidad = null)
        {
            if (cantidad is < 0) throw new ArgumentOutOfRangeException(nameof(cantidad));
            return new(true, cantidad);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Tiene;
            if (Cantidad is not null) yield return Cantidad;
        }
    }

}


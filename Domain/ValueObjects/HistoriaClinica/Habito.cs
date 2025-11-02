using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Base;

// Hábitos con cantidad (p. ej. cigarrillos por día / vasos por semana)

namespace Domain.ValueObjects
{
    public class Habito : BaseValueObject
    {
        private Habito()
        {
        }
        public Habito(bool tiene, int? cantidad)
        {
            Tiene = tiene;
            Cantidad = tiene ? cantidad : null;
        }

        public bool Tiene { get; protected set; }
        public int? Cantidad { get; protected set; }

        public static Habito No() => new(false, null);

        public static Habito Si(int? cantidad = null)
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


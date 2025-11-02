using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Base;

namespace Domain.ValueObjects
{
    // Un sí/no con fecha (ej. radiografías)
    public class RespuestaConFecha : BaseValueObject
    {
        private RespuestaConFecha()
        {
        }

        public RespuestaConFecha(bool si, DateTime? fecha)
        {
            Si = si;
            Fecha = si ? fecha : null;
        }

        public bool Si { get; protected set; }
        public DateTime? Fecha { get; protected set; }
  
        public static RespuestaConFecha No() => new(false, null);

        public static RespuestaConFecha SiEn(DateTime fecha)
            => new(true, fecha);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Si;
            if (Fecha is not null) yield return Fecha;
        }
    }


}

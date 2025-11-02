using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Base;

namespace Domain.ValueObjects
{
 
    // Un sí/no con un detalle opcional (texto)
    public class RespuestaConDetalle : BaseValueObject
    {

        private RespuestaConDetalle()
        {

        }
        public RespuestaConDetalle(bool si, string? detalle)
        {
            Si = si;
            Detalle = string.IsNullOrWhiteSpace(detalle) ? null : detalle.Trim();
        }

        public bool Si { get; protected set; }
        public string? Detalle { get; protected set; }


        // Si es SI y el detalle es requerido, validamos en fábrica
        public static RespuestaConDetalle SiCon(string? detalle, bool detalleObligatorio = false)
        {
            if (detalleObligatorio && string.IsNullOrWhiteSpace(detalle))
                throw new ArgumentException("Se requiere detalle.");
            return new(true, detalle);
        }

        public static RespuestaConDetalle No() => new(false, null);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Si;
            if (Detalle is not null) yield return Detalle;
        }
    }

}

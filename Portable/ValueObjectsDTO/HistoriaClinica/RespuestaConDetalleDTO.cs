using Shared.Portable.Base;

namespace Portable.ValueObjects
{
 
    // Un sí/no con un detalle opcional (texto)
    public class RespuestaConDetalleDTO : BaseValueObjectDTO
    {

        private RespuestaConDetalleDTO()
        {

        }
        public RespuestaConDetalleDTO(bool si, string? detalle)
        {
            Si = si;
            Detalle = string.IsNullOrWhiteSpace(detalle) ? null : detalle.Trim();
        }

        public bool Si { get; set; }
        public string? Detalle { get; set; }


        // Si es SI y el detalle es requerido, validamos en fábrica
        public static RespuestaConDetalleDTO SiCon(string? detalle, bool detalleObligatorio = false)
        {
            if (detalleObligatorio && string.IsNullOrWhiteSpace(detalle))
                throw new ArgumentException("Se requiere detalle.");
            return new(true, detalle);
        }

        public static RespuestaConDetalleDTO No() => new(false, null);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Si;
            if (Detalle is not null) yield return Detalle;
        }
    }

}

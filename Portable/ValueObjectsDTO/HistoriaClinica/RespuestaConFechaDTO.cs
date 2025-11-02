using Shared.Portable.Base;

namespace Portable.ValueObjects
{
    // Un sí/no con fecha (ej. radiografías)
    public class RespuestaConFechaDTO : BaseValueObjectDTO
    {
        private RespuestaConFechaDTO()
        {
        }

        public RespuestaConFechaDTO(bool si, DateTime? fecha)
        {
            Si = si;
            Fecha = si ? fecha : null;
        }

        public bool Si { get; set; }
        public DateTime? Fecha { get; set; }
  
        public static RespuestaConFechaDTO No() => new(false, null);

        public static RespuestaConFechaDTO SiEn(DateTime fecha)
            => new(true, fecha);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Si;
            if (Fecha is not null) yield return Fecha;
        }
    }


}

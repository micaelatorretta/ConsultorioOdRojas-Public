using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Portable.FunctionalUnits.Pacientes.DTOs;
using Portable.ValueObjectsDTO;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Turnos.DTOs
{
    public class TurnoDTO : BaseDTO
    {
        public TurnoDTO() { }

        public string Descripcion { get; set; } = string.Empty;
        public PacienteDTO? Paciente { get; set; } 

        public DateOnly Fecha { get; set; }

        [JsonConverter(typeof(TimeOnlyConverter))]
        public TimeOnly HoraInicio { get; set; }

        [JsonConverter(typeof(TimeOnlyConverter))]
        public TimeOnly HoraFin { get; set; }

        public string PacienteName { get; set; } = string.Empty;

        #region FKs
        public int PacienteId { get; set; }
        #endregion

        //public string HoraInicio { get; set; } = string.Empty;
        //public string HoraFin { get; set; } = string.Empty;

    }


    public class TimeOnlyConverter : JsonConverter<TimeOnly>
    {
        private const string Format = "HH:mm";

        public override void WriteJson(JsonWriter writer, TimeOnly value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString(Format));
        }

        public override TimeOnly ReadJson(JsonReader reader, Type objectType, TimeOnly existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var str = reader.Value?.ToString();
            if (string.IsNullOrEmpty(str))
                return default;

            // admite HH:mm y HH:mm:ss
            if (TimeOnly.TryParseExact(str, new[] { "HH:mm", "HH:mm:ss" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
                return result;

            throw new JsonSerializationException($"Formato de hora inválido: {str}");
        }
    }

}

using Portable.FunctionalUnits.Pacientes.DTOs;
using Portable.ValueObjects;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.HistoriasClinicas.DTOs
{
    public class HistoriaClinicaDTO : BaseDTO
    {
        public HistoriaClinicaDTO() { }
        public PacienteDTO? Paciente { get; set; }
        public int PacienteId { get; set; }
        public RespuestaConDetalleDTO IntervencionesUlt2Anios { get; set; } = RespuestaConDetalleDTO.No();
        public RespuestaConDetalleDTO TratamientoMedico { get; set; } = RespuestaConDetalleDTO.No();
        public RespuestaConDetalleDTO Alergias { get; set; } = RespuestaConDetalleDTO.No();
        public RespuestaConDetalleDTO AnestesiasPrevias { get; set; } = RespuestaConDetalleDTO.No();
        public RespuestaConDetalleDTO SangradoExcesivo { get; set; } = RespuestaConDetalleDTO.No();
        public bool CansancioAlCaminar { get; set; } = false;

        public HabitoDTO Fuma { get; set; } = HabitoDTO.No(); // ¿Cuántos?
        public HabitoDTO Bebe { get; set; } = HabitoDTO.No(); // ¿Cuánto?
        public bool Embarazo { get; set; } = false;


        //public Afecciones Afecciones { get;  set; } = new(AfeccionFlags.Ninguna);
        public RespuestaConDetalleDTO MedicacionActual { get; set; } = RespuestaConDetalleDTO.No();
        public RespuestaConFechaDTO Radiografias { get; set; } = RespuestaConFechaDTO.No();

        public string? Observaciones { get; set; }


    }
}

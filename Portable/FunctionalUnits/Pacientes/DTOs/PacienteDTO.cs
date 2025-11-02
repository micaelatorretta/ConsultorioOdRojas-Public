using Portable.Enums.Auth;
using Portable.FunctionalUnits.HistoriasClinicas.DTOs;
using Portable.FunctionalUnits.ObrasSociales.DTOs;
using Portable.FunctionalUnits.Odontogramas.DTOs;
using Portable.ValueObjectsDTO;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Pacientes.DTOs
{
    public class PacienteDTO : BaseDTO
    {
        public PacienteDTO() { }

        public HistoriaClinicaDTO? HistoriaClinica { get; set; }
        public ObraSocialDTO? ObraSocial { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string DNI { get; set; } = string.Empty;
        public List<OdontogramaDTO> Odontogramas { get; set; } = new();

        #region FKs
        public int? ObraSocialId { get; set; }
        #endregion

    }
}

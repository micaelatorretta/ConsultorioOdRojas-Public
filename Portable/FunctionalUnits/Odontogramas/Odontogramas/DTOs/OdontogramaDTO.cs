using Portable.FunctionalUnits.Pacientes.DTOs;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Odontogramas.DTOs
{
    public class OdontogramaDTO : BaseDTO
    {
        public OdontogramaDTO() { }
        public PacienteDTO Paciente { get; set; } = null!;
        /// <summary>
        /// Representa que es el odontograma vigente del paciente.
        /// Es decir, que es el odontograma que está abierto a edición actualmente.
        /// </summary>
        public bool Vigente { get; set; }
        /// <summary>
        /// Piezas dentales en las que se realizó una prestación.
        /// </summary>
        public List<OdontogramaPiezaDentalDTO> PiezasDentales { get; set; } = new();

    }
}

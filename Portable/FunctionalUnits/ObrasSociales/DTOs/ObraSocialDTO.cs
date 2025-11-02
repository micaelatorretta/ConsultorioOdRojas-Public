using Portable.ValueObjectsDTO;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.ObrasSociales.DTOs
{
    public class ObraSocialDTO : BaseDTO
    {
        public ObraSocialDTO() { }

        public string Nombre { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;


    }
}

using Newtonsoft.Json;
using Portable.Enums;
using Shared.Portable.Base;

namespace Portable.ValueObjectsDTO
{
    public class ContactoTelefonicoDTO : BaseValueObjectDTO
    {
        public string NumeroTelefono { get; protected set; } = string.Empty;
        public TipoTelefono Tipo { get; protected set; } 
        public string? Extension { get; protected set; }

        // Constructor privado para la creación controlada
        // El ValueObject no deberia poder instanciarse vacio.
        private ContactoTelefonicoDTO() { }

        // Constructor con validaciones para crear un ContactoTelefonico
        [JsonConstructor]
        public ContactoTelefonicoDTO(string numeroTelefono, TipoTelefono tipo, string? extension = null)
        {
            NumeroTelefono = ValidarNumeroTelefono(numeroTelefono);
            Tipo = tipo;
            Extension = extension;
        }

        // Método para validar el formato del número de teléfono
        private string ValidarNumeroTelefono(string numeroTelefono)
        {
            if (string.IsNullOrWhiteSpace(numeroTelefono))
            {
                throw new ArgumentException("El número de teléfono no puede estar vacío.", nameof(numeroTelefono));
            }

            //// Validación de formato para números internacionales
            //var regex = new Regex(@"^\+\d{1,3}[- ]?\d{1,14}$");
            //if (!regex.IsMatch(numeroTelefono))
            //{
            //    throw new ArgumentException("El número de teléfono no tiene un formato válido.");
            //}

            return numeroTelefono;
        }

        // Método para comparar dos ContactoTelefonico (Equals)
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return NumeroTelefono;
            yield return Tipo;
            if (Extension is not null) yield return Extension;
        }

        // Método de utilida

    }
}
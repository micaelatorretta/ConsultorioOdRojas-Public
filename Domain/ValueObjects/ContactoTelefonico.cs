using Portable.Enums;
using Shared.Domain.Base;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public class ContactoTelefonico : BaseValueObject
    {
        public string NumeroTelefono { get; protected set; } = string.Empty;
        public TipoTelefono Tipo { get; protected set; }
        public string? Extension { get; protected set; }

        // Constructor privado para la creación controlada
        // El ValueObject no deberia poder instanciarse vacio.
        private ContactoTelefonico() { }

        // Constructor con validaciones para crear un ContactoTelefonico
        public ContactoTelefonico(string numeroTelefono, TipoTelefono tipoTelefono, string? extension = null)
        {
            NumeroTelefono = ValidarNumeroTelefono(numeroTelefono);
            Tipo = tipoTelefono;
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
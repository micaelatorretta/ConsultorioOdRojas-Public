using Portable.Enums.Auth;
using Shared.Domain.Base;
using Domain.FunctionalUnits.Usuarios.Rules;
using Shared.Domain.Extensions;

namespace Domain.FunctionalUnits.Usuarios.Entities
{
    public partial class Usuario : BaseAggregateEntity
    {
        public Usuario() { }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
        public UsuarioRol Rol { get; set; }

    }
    public partial class Usuario
    {
        public override async Task Validate()
        {
            Rules.AddRules(new()
            { 
                // Se manda la Entidad Usuario (this) para validar.
                new DatosObligatoriosUsuarioRule(this)
            });

            // Verifica que se cumplan las reglas y si hay un fallo lanza
            // una excepcion del tipo BusinessRuleException.
            Rules.CheckRules();
            await Task.CompletedTask;
        }
    }
}

using Portable.Enums.Auth;
using Portable.ValueObjectsDTO;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Usuarios.DTOs
{
    public class UsuarioDTO : BaseDTO
    {
        public UsuarioDTO() { }

        public string Username { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UsuarioRol Rol { get; set; }
        public string Salt { get; set; } = string.Empty;

        /// <summary>
        /// Indica si el usuario cambió la contraseña.
        /// </summary>
        public bool ChangePassword { get; set; } = false;

    }
}

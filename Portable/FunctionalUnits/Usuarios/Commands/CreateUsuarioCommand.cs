using Portable.FunctionalUnits.Usuarios.DTOs;
using Portable.FunctionalUnits.Usuarios.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Usuarios.Commands
{
    public class CreateUsuarioCommand : BaseCommand<CreateUsuarioResponse>
    {
        public CreateUsuarioCommand() { }

        public UsuarioDTO Usuario { get; set; } = null!;
    }
}
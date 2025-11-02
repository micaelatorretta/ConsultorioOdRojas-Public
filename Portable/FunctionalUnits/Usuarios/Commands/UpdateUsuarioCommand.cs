using Portable.FunctionalUnits.Usuarios.DTOs;
using Portable.FunctionalUnits.Usuarios.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Usuarios.Commands
{
    public class UpdateUsuarioCommand : BaseCommand<UpdateUsuarioResponse>
    {
        public UpdateUsuarioCommand() { }

        public UsuarioDTO Usuario { get; set; } = null!;
    }
}

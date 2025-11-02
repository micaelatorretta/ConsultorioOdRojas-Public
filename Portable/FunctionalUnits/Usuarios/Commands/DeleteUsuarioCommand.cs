using Portable.FunctionalUnits.Usuarios.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Usuarios.Commands
{
    public class DeleteUsuarioCommand : BaseCommand<DeleteUsuarioResponse>
    {
        public int Id { get; set; }

        public DeleteUsuarioCommand() { }

    }
}
using Application.FunctionalUnits.Auth.Commands.Strategies;
using Domain.FunctionalUnits.Usuarios.Entities;
using Portable.FunctionalUnits.Usuarios.Commands;
using Portable.FunctionalUnits.Usuarios.DTOs;
using Portable.FunctionalUnits.Usuarios.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Application.Utils;

namespace Application.FunctionalUnits.Usuarios.Commands
{
    public class CreateUsuarioCommandHandler : BaseCommandHandler<CreateUsuarioCommand, CreateUsuarioResponse>
    {
        private Usuario _usuario { get; set; } = null!;

        public CreateUsuarioCommandHandler(IWorkContext workContext) : base(workContext)
        {
        }

        public override async Task<CreateUsuarioResponse> Handle(CreateUsuarioCommand command, CancellationToken cancellationToken)
        {
            var response = new CreateUsuarioResponse();

            _usuario = WorkContext.Services.Mapper.Map<Usuario>(command.Usuario);

            // Devuelve una tupla con la contraseña hasheada y el salt.
            var (hashedPassword, salt) = await em.RunAsync<(string HashedPassword, string Salt)>(new HashPasswordStrategy(WorkContext),
                                                                                                 new HashPasswordRecord(command.Usuario.Password));
            _usuario.Password = hashedPassword;
            _usuario.Salt = salt;

            await em.CreateAsync(_usuario);

            // Suscribirse a los eventos
            WorkContext.OnSuccess += HandleSuccess;
            WorkContext.OnFailure += HandleFailure;

            return response;
        }

        /// <summary>
        /// Handler que maneja una respuesta satisfactoria.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleSuccess(object? sender, OnSuccessEventArgs<object> e)
        {
            var response = (e.Response as CreateUsuarioResponse);

            // Por referencia se setea lo siguiente en la response:
            response!.Usuario = WorkContext.Services.Mapper.Map<UsuarioDTO>(_usuario);

        }

        /// <summary>
        /// Handler para manejar valores en la response si hubo un error.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleFailure(object? sender, OnFailureEventArgs e)
        {
            // Lógica que quieres que se ejecute en caso de fallo
            Console.WriteLine($"Handler: Operación fallida con error: {e.Exception.Message}");
        }
    }
}

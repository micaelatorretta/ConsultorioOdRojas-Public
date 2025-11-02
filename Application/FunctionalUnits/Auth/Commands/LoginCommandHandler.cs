using Application.FunctionalUnits.Auth.Commands.Strategies;
using Domain.FunctionalUnits.Auth.Interfaces;
using Domain.FunctionalUnits.Usuarios.Entities;
using Portable.FunctionalUnits.Auth.Commands;
using Portable.FunctionalUnits.Auth.Responses;
using Portable.FunctionalUnits.Usuarios.DTOs;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Application.Utils;

namespace Application.FunctionalUnits.Auth.Commands
{
    public class LoginCommandHandler : BaseCommandHandler<LoginCommand, LoginResponse>
    {
        private Usuario? _usuario { get; set; } 

        public LoginCommandHandler(IWorkContext workContext) : base(workContext)
        {
        }

        public override async Task<LoginResponse> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var response = new LoginResponse();

            var repository = WorkContext.Services.ReadOnlyUnitOfWork.GetRepository<Usuario, IAuthRepository>();

           
            _usuario = await repository.LoginAsync(command.LoginCredentials.Username);

            if(_usuario is null)
            {
                throw new Exception("Usuario no encontrado.");
            }

            var passwordIsValid = await em.RunAsync<bool>(new VerifyPasswordStrategy(WorkContext),
                                                          new VerifyPasswordRecord(command.LoginCredentials.Password, // contraseña que llega desde el cliente
                                                          _usuario.Password,// contraseña hasheada
                                                          _usuario.Salt));

            if (!passwordIsValid)
            {
                throw new Exception("Credenciales no validas.");
            }
            // Suscribirse a los eventos
            WorkContext.OnSuccess += HandleSuccess;

            return response;
        }

        /// <summary>
        /// Handler que maneja una respuesta satisfactoria.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleSuccess(object? sender, OnSuccessEventArgs<object> e)
        {
            var response = (e.Response as LoginResponse);

            // Por referencia se setea lo siguiente en la response:
            response!.Usuario = WorkContext.Services.Mapper.Map<UsuarioDTO>(_usuario);

        }

    }
}

using System.Runtime.InteropServices;
using Application.FunctionalUnits.Auth.Commands.Strategies;
using Domain.FunctionalUnits.EntidadDummies.Entities;
using Domain.FunctionalUnits.Usuarios.Entities;
using Portable.FunctionalUnits.EntidadDummies.Commands;
using Portable.FunctionalUnits.EntidadDummies.DTOs;
using Portable.FunctionalUnits.EntidadDummies.Responses;
using Portable.FunctionalUnits.Usuarios.Commands;
using Portable.FunctionalUnits.Usuarios.DTOs;
using Portable.FunctionalUnits.Usuarios.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Application.Utils;
using Shared.Portable.Enums.EntityState;

namespace Application.FunctionalUnits.Usuarios.Commands
{
    public class UpdateUsuarioCommandHandler : BaseCommandHandler<UpdateUsuarioCommand, UpdateUsuarioResponse>
    {
        private Usuario _usuario { get; set; } = null!;

        public UpdateUsuarioCommandHandler(IWorkContext workContext) : base(workContext)
        {
        }


        public override async Task<UpdateUsuarioResponse> Handle(UpdateUsuarioCommand command, CancellationToken cancellationToken)
        {
            var response = new UpdateUsuarioResponse();

            _usuario = WorkContext.Services.Mapper.Map<Usuario>(command.Usuario);

            // Hashea solo cuando el usuario cambió la contraseña
            if (command.Usuario.ChangePassword)
            {
                // Devuelve una tupla con la contraseña hasheada y el salt.
                var (hashedPassword, salt) = await em.RunAsync<(string HashedPassword, string Salt)>(new HashPasswordStrategy(WorkContext),
                                                                                                     new HashPasswordRecord(command.Usuario.Password));
                _usuario.Password = hashedPassword;
                _usuario.Salt = salt;

            }

            _usuario.EntityState = EntityStateMark.Modified;
            // Update donde se tiene que personalizar el GetById en un repositorio especifico.
            // Para asi poder trackear las entidades
            await em.UpdateAsync(_usuario);

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
            var response = (e.Response as UpdateUsuarioResponse);

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

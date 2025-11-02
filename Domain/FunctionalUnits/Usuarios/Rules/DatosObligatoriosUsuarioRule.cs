using Domain.FunctionalUnits.Usuarios.Entities;
using Shared.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.FunctionalUnits.Usuarios.Rules
{
    public class DatosObligatoriosUsuarioRule : BaseBusinessRule
    {
        private Usuario _usuario { get; set; }
        public DatosObligatoriosUsuarioRule(Usuario usuario)
        {
            _usuario = usuario;
        }
        public override bool IsBroken()
        {
            if (string.IsNullOrEmpty(_usuario.Nombre))
            {
                AddErrorMessage($"El {nameof(Usuario.Nombre)} del {nameof(Usuario)} es obligatorio.");
            }
            if (string.IsNullOrEmpty(_usuario.Apellido))
            {
                AddErrorMessage($"El {nameof(Usuario.Apellido)} del {nameof(Usuario)} es obligatorio.");
            }

            if (string.IsNullOrEmpty(_usuario.Email))
            {
                AddErrorMessage($"El {nameof(Usuario.Email)} del {nameof(Usuario)} es obligatorio.");
            }

            if (string.IsNullOrEmpty(_usuario.Username))
            {
                AddErrorMessage($"El {nameof(Usuario.Username)} del {nameof(Usuario)} es obligatorio.");
            }
            
            if (string.IsNullOrEmpty(_usuario.Password))
            {
                AddErrorMessage($"La contraseña del {nameof(Usuario)} es obligatoria.");
            }

            return HasErrorMessages();
        }
    }
}

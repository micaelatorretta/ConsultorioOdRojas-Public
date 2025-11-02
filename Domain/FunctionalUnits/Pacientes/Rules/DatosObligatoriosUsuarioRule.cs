using Domain.FunctionalUnits.Pacientes.Entities;
using Shared.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.FunctionalUnits.Pacientes.Rules
{
    public class DatosObligatoriosPacienteRule : BaseBusinessRule
    {
        private Paciente _Paciente { get; set; }
        public DatosObligatoriosPacienteRule(Paciente Paciente)
        {
            _Paciente = Paciente;
        }
        public override bool IsBroken()
        {
            if (string.IsNullOrEmpty(_Paciente.Nombre))
            {
                AddErrorMessage($"El {nameof(Paciente.Nombre)} del {nameof(Paciente)} es obligatorio.");
            }
            if (string.IsNullOrEmpty(_Paciente.Apellido))
            {
                AddErrorMessage($"El {nameof(Paciente.Apellido)} del {nameof(Paciente)} es obligatorio.");
            }

     
            ValidarDNI();

            return HasErrorMessages();
        }

        private void ValidarDNI()
        {
            if (string.IsNullOrEmpty(_Paciente.DNI))
            {
                AddErrorMessage($"El {nameof(Paciente.DNI)} del {nameof(Paciente)} es obligatorio.");
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(_Paciente.DNI, @"^\d{8}$"))
            {
                AddErrorMessage($"El {nameof(Paciente.DNI)} del {nameof(Paciente)} debe contener exactamente 8 dígitos numéricos.");
            }
        }
    }
}

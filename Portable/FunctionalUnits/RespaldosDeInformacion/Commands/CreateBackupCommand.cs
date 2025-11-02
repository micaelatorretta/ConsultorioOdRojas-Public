using Portable.FunctionalUnits.ObrasSociales.DTOs;
using Portable.FunctionalUnits.ObrasSociales.Responses;
using Portable.FunctionalUnits.RespaldosDeInformacion.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.RespaldosDeInformacion.Commands
{
    public class CreateBackupCommand : BaseCommand<CreateBackupResponse>
    {
        public CreateBackupCommand() { }

        /// <summary>
        /// Ruta del directorio donde se guardará el backup.
        /// Inicialmente se guardará en API/wwwRoot.
        /// </summary>
        public string? WebRootPath { get; set; }    

        /// <summary>
        /// Representa a la URL de la aplicación.
        /// Se va a utilizar para la response al cliente, para que pueda
        /// descargar el archivo backup generado.
        /// </summary>
        public string? Url { get; set; }    
    }
}
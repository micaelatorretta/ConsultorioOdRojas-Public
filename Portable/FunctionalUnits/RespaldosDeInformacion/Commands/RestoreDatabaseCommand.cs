using Microsoft.AspNetCore.Http;
using Portable.FunctionalUnits.ObrasSociales.DTOs;
using Portable.FunctionalUnits.ObrasSociales.Responses;
using Portable.FunctionalUnits.RespaldosDeInformacion.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.RespaldosDeInformacion.Commands
{
    public class RestoreDatabaseCommand : BaseCommand<RestoreDatabaseResponse>
    {
        public RestoreDatabaseCommand() { }

        public IFormFile BackupFile { get; set; } = null!;
    }
}
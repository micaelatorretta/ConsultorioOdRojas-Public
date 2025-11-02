using System.IO.Compression;
using Application.FunctionalUnits.Auth.Commands.Strategies;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Portable.FunctionalUnits.RespaldosDeInformacion.Commands;
using Portable.FunctionalUnits.RespaldosDeInformacion.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Application.Utils;

namespace Application.FunctionalUnits.RespaldosDeInformacion.Commands
{
    public class RestoreDatabaseCommandHandler : BaseCommandHandler<RestoreDatabaseCommand, RestoreDatabaseResponse>
    {
        public RestoreDatabaseCommandHandler(IWorkContext workContext) : base(workContext)
        {
        }

        public override async Task<RestoreDatabaseResponse> Handle(RestoreDatabaseCommand command, CancellationToken cancellationToken)
        {
            var response = new RestoreDatabaseResponse();

            var file = command.BackupFile;

            if (file == null || file.Length == 0)
                throw new Exception("No se ha cargado ningún archivo.");


            await WorkContext.Services.UnitOfWork.RestoreDatabaseAsync(command.BackupFile);


            return response;
        }

    }
}

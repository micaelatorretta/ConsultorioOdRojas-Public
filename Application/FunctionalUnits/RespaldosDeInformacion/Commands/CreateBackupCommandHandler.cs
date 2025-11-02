using Application.FunctionalUnits.Auth.Commands.Strategies;
using Portable.FunctionalUnits.RespaldosDeInformacion.Commands;
using Portable.FunctionalUnits.RespaldosDeInformacion.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Application.Utils;

namespace Application.FunctionalUnits.RespaldosDeInformacion.Commands
{
    public class CreateBackupCommandHandler : BaseCommandHandler<CreateBackupCommand, CreateBackupResponse>
    {
        public CreateBackupCommandHandler(IWorkContext workContext) : base(workContext)
        {
        }

        public override async Task<CreateBackupResponse> Handle(CreateBackupCommand command, CancellationToken cancellationToken)
        {
            var response = new CreateBackupResponse();

            if (string.IsNullOrEmpty(command.WebRootPath) || string.IsNullOrEmpty(command.Url))
            {
                return response;
            }

            //Genera el backup y devuelve la URL del archivo comprimido
            var urlArchivoComprimido = await em.RunAsync<string>(new GenerarBackupStrategy(WorkContext),
                                                         new GenerarBackupRecord(command.WebRootPath,
                                                         command.Url));

            response.UrlArchivoComprimido = urlArchivoComprimido;

            return response;
        }

    }
}

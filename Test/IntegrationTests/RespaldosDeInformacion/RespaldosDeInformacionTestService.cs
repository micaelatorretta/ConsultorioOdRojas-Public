using Microsoft.AspNetCore.Mvc.Testing;
using Portable.FunctionalUnits.RespaldosDeInformacion.Commands;
using Portable.FunctionalUnits.RespaldosDeInformacion.Responses;
using Test.IntegrationTests.Base;

namespace Test.IntegrationTests.RespaldosDeInformacion
{
    public class RespaldosDeInformacionTestService : BaseTestService, IDisposable
    {
        private const string URL_CREATE_BACKUP = "/RespaldosDeInformacion/CreateBackup";

        public RespaldosDeInformacionTestService(WebApplicationFactory<Program> factory) : base(factory)
        {
        }

        #region Actions

        #region Commands
        public async Task<CreateBackupResponse> CreateBackup()
        {
            var command = new CreateBackupCommand();

            var response = await SendRequestAndDeserializeResponse<CreateBackupCommand, CreateBackupResponse>(URL_CREATE_BACKUP, command);
            
            return response;
        }

        #endregion

        #endregion

        public void Dispose()
        {
            if (cleanUp)
            {
            }
        }
    }
}

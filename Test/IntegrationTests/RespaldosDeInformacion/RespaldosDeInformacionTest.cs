using Microsoft.AspNetCore.Mvc.Testing;
using Shared.Portable.Enums.EntityState;
using Test.IntegrationTests.Base;

namespace Test.IntegrationTests.RespaldosDeInformacion
{
    public class RespaldosDeInformacionTest : BaseTest, IDisposable
    {
        protected RespaldosDeInformacionTestService _service;
        public RespaldosDeInformacionTest(WebApplicationFactory<Program> factory) 
        {
            _service = new(factory);
        }

        #region Commands
        [Fact]
        public async Task Create_Backup()
        {
            // Act
            var commandResponse = await _service.CreateBackup();

            Assert.True(commandResponse.Success, commandResponse.StackTrace);
        }

        #endregion

        public void Dispose()
        {
            _service.Dispose();
        }
    }
}

using Microsoft.AspNetCore.Mvc.Testing;
using Shared.Portable.Enums.EntityState;
using Test.IntegrationTests.Base;
using Test.IntegrationTests.Usuarios;

namespace Test.IntegrationTests.Auth
{
    public class AuthTest : BaseTest, IDisposable
    {
        protected AuthTestService _authService;
        protected UsuarioTestService _usuarioService;

        public AuthTest(WebApplicationFactory<Program> factory)
        {
            _authService = new(factory);
            _usuarioService = new(factory);
        }

        #region Commands
        [Fact]
        public async Task Login()
        {

            // Arrange

            // Crear usuario de prueba
            var usuarioClient = _usuarioService.GetUsuario();
            var usuarioCreated = (await _usuarioService.Create(usuarioClient)).Usuario;

            var credentials = _authService.GetLoginCredentials(usuarioClient!.Username, usuarioClient.Password);

            // Act
            var commandLoginResponse = await _authService.Login(credentials);

            // Assert
            Assert.True(commandLoginResponse.Success, commandLoginResponse.StackTrace);
            Assert.Equal(usuarioCreated!.Id, commandLoginResponse.Usuario!.Id); // Asegurar de que el ID sea el esperado
        }

        #endregion

        public void Dispose()
        {
            _authService.Dispose();
        }
    }
}

using Microsoft.AspNetCore.Mvc.Testing;
using Shared.Portable.Enums.EntityState;
using Test.IntegrationTests.Base;

namespace Test.IntegrationTests.ObrasSociales
{
    public class ObraSocialTest : BaseTest, IDisposable
    {
        protected ObraSocialTestService _service;
        public ObraSocialTest(WebApplicationFactory<Program> factory) 
        {
            _service = new(factory);
        }

        #region Commands
        [Fact]
        public async Task Create_ObraSocial()
        {
            // Arrange
            var ObraSocial = _service.GetObraSocial();

            // Act
            var commandResponse = await _service.Create(ObraSocial);

            Assert.True(commandResponse.Success, commandResponse.StackTrace);

            var queryResponse = await _service.GetById(commandResponse.ObraSocial!.Id);

            // Assert
            Assert.NotNull(queryResponse.ObraSocial);
            Assert.Equal(queryResponse.ObraSocial.Id, commandResponse.ObraSocial.Id); // Asegurar de que el ID sea el esperado
        }

        [Fact]
        public async Task Update_ObraSocial()
        {
            // Arrange
            var ObraSocial = _service.GetObraSocial();


            // Act
            ObraSocial = (await _service.Create(ObraSocial)).ObraSocial;

            // Marco la entidad como modificada aunque al IAggregateRoot no hace falta marcarlo (es a modo de ejemplo).
            ObraSocial!.EntityState = EntityStateMark.Modified;
            ObraSocial.Nombre = "Swiss Medical";

            var commandResponse = await _service.Update(ObraSocial);
            Assert.True(commandResponse.Success, commandResponse.Message);

            var queryResponse = await _service.GetById(commandResponse.ObraSocial!.Id);

            // Assert
            Assert.NotNull(queryResponse.ObraSocial);
            Assert.Equal(queryResponse.ObraSocial.Nombre, commandResponse.ObraSocial.Nombre); // Asegurar de que el ID sea el esperado
        }

        [Fact]
        public async Task Delete_ObraSocial()
        {
            // Arrange
            var ObraSocial = _service.GetObraSocial();

            // Act
            ObraSocial = (await _service.Create(ObraSocial)).ObraSocial;

            var commandResponse = await _service.Delete(ObraSocial!.Id);

            var queryResponse = await _service.GetById(ObraSocial!.Id);

            // Assert
            Assert.True(queryResponse.ObraSocial is null);
        }
        #endregion

        #region Queries
        [Fact]
        public async Task GetById_ObraSocial()
        {
            // Arrange
            var obraSocial = _service.GetObraSocial();
             obraSocial = (await _service.Create(obraSocial)).ObraSocial;

            // Arrange y Act se hacen dentro del service.
            // Obtengo ObraSocial con id = 2
            var response = await _service.GetById(obraSocial!.Id);

            // Assert
            Assert.NotNull(response?.ObraSocial);
            Assert.Equal(obraSocial.Id, response.ObraSocial.Id); // Asegurar de que el ID sea el esperado
        }

        [Fact]
        public async Task GetPaged_ObraSocial()
        {
            var obraSocial = _service.GetObraSocial();
            obraSocial = (await _service.Create(obraSocial)).ObraSocial;

            // Arrange y Act se hacen dentro del service.
            // Obtengo un listado de ObraSocial paginado.
            // Posee parametros opcionales para el numero de pagina y el tamaño de pagina.
            var response = await _service.GetPaged();

            // Assert
            Assert.True(response.Success, response.Message);
            Assert.True(response.PaginationData.Items.Any(), "La query para obtener los datos paginados no devolvió ningún item"); 
        }


        #endregion

        public void Dispose()
        {
            _service.Dispose();
        }
    }
}

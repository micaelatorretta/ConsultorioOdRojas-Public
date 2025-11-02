using Microsoft.AspNetCore.Mvc.Testing;
using Portable.Enums;
using Shared.Portable.Enums.EntityState;
using Test.IntegrationTests.Base;

namespace Test.IntegrationTests.Prestaciones
{
    public class NomencladorTest : BaseTest, IDisposable
    {
        protected NomencladorTestService _service;
        public NomencladorTest(WebApplicationFactory<Program> factory) 
        {
            _service = new(factory);
        }

        #region Commands
        [Fact]
        public async Task Create_Nomenclador()
        {
            // Arrange
            var Nomenclador = _service.GetNomenclador();

            // Act
            var commandResponse = await _service.Create(Nomenclador);

            Assert.True(commandResponse.Success, commandResponse.StackTrace);

            var queryResponse = await _service.GetById(commandResponse.Nomenclador!.Id);

            // Assert
            Assert.NotNull(queryResponse.Nomenclador);
            Assert.Equal(queryResponse.Nomenclador.Id, commandResponse.Nomenclador.Id); // Asegurar de que el ID sea el esperado
        }

        [Fact]
        public async Task Update_Nomenclador()
        {
            // Arrange
            var Nomenclador = _service.GetNomenclador();


            // Act
            Nomenclador = (await _service.Create(Nomenclador)).Nomenclador;

            // Marco la entidad como modificada aunque al IAggregateRoot no hace falta marcarlo (es a modo de ejemplo).
            Nomenclador!.EntityState = EntityStateMark.Modified;
            Nomenclador.Descripcion = _service.RandomGenerator.GenerateRandomString(40);

            var commandResponse = await _service.Update(Nomenclador);
            Assert.True(commandResponse.Success, commandResponse.Message);

            var queryResponse = await _service.GetById(commandResponse.Nomenclador!.Id);

            // Assert
            Assert.NotNull(queryResponse.Nomenclador);
            Assert.Equal(queryResponse.Nomenclador.Descripcion, commandResponse.Nomenclador.Descripcion); // Asegurar de que el ID sea el esperado
        }

        [Fact]
        public async Task Delete_Nomenclador()
        {
            // Arrange
            var Nomenclador = _service.GetNomenclador();

            // Act
            Nomenclador = (await _service.Create(Nomenclador)).Nomenclador;

            var commandResponse = await _service.Delete(Nomenclador!.Id);

            var queryResponse = await _service.GetById(Nomenclador!.Id);

            // Assert
            Assert.True(queryResponse.Nomenclador is null);
        }
        #endregion

        #region Queries
        [Fact]
        public async Task GetById_Nomenclador()
        {
            // Arrange
            var Nomenclador = _service.GetNomenclador();
             Nomenclador = (await _service.Create(Nomenclador)).Nomenclador;

            // Arrange y Act se hacen dentro del service.
            // Obtengo Nomenclador con id = 2
            var response = await _service.GetById(Nomenclador!.Id);

            // Assert
            Assert.NotNull(response?.Nomenclador);
            Assert.Equal(Nomenclador.Id, response.Nomenclador.Id); // Asegurar de que el ID sea el esperado
        }

        [Fact]
        public async Task GetPaged_Nomenclador()
        {
            var Nomenclador = _service.GetNomenclador();
            Nomenclador = (await _service.Create(Nomenclador)).Nomenclador;

            // Arrange y Act se hacen dentro del service.
            // Obtengo un listado de Nomenclador paginado.
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

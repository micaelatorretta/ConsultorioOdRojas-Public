using Microsoft.AspNetCore.Mvc.Testing;
using Shared.Portable.Enums.EntityState;
using Test.IntegrationTests.Base;

namespace Test.IntegrationTests.EntidadDummies
{
    public class EntidadDummyTest : BaseTest, IDisposable
    {
        protected EntidadDummyTestService _service;
        public EntidadDummyTest(WebApplicationFactory<Program> factory) 
        {
            _service = new(factory);
        }

        #region Commands
        [Fact]
        public async Task Create_EntidadDummy()
        {
            // Arrange
            var entidadDummy = _service.GetEntidadDummy();

            // Act
            var commandResponse = await _service.Create(entidadDummy);

            Assert.True(commandResponse.Success, commandResponse.StackTrace);

            var queryResponse = await _service.GetById(commandResponse.EntidadDummy!.Id);

            // Assert
            Assert.NotNull(queryResponse.EntidadDummy);
            Assert.Equal(queryResponse.EntidadDummy.Id, commandResponse.EntidadDummy.Id); // Asegurar de que el ID sea el esperado
        }

        //[Fact]
        //public async Task CreateWithError_EntidadDummy()
        //{
        //    // Arrange
        //    var entidadDummy = _service.GetEntidadDummy();
        //    entidadDummy.Name = string.Empty;
        //    // Act
        //    var commandResponse = await _service.Create(entidadDummy);

        //    Assert.True(commandResponse.Success, commandResponse.StackTrace);

        //    var queryResponse = await _service.GetById(commandResponse.EntidadDummy!.Id);

        //    // Assert
        //    Assert.NotNull(queryResponse.EntidadDummy);
        //    Assert.Equal(queryResponse.EntidadDummy.Id, commandResponse.EntidadDummy.Id); // Asegurar de que el ID sea el esperado
        //}

        [Fact]
        public async Task Update_EntidadDummy()
        {
            // Arrange
            var entidadDummy = _service.GetEntidadDummy();

            // Act
            entidadDummy = (await _service.Create(entidadDummy)).EntidadDummy;

            // Marco la entidad como modificada aunque al IAggregateRoot no hace falta marcarlo (es a modo de ejemplo).
            entidadDummy!.EntityState = EntityStateMark.Modified;
            
            // Modifico la EntidadDummyC
            entidadDummy.DummyC.Name = "DummyC" + DateTime.UtcNow.ToShortDateString();
            entidadDummy.DummyC.EntityState = EntityStateMark.Modified;

            // Agrego un elemento a la lista de EntidadDummyB.
            entidadDummy.DummiesB = new() { _service.GetEntidadDummyB() };

            var commandResponse = await _service.Update(entidadDummy);

            var queryResponse = await _service.GetById(commandResponse.EntidadDummy!.Id);

            // Assert
            Assert.NotNull(queryResponse.EntidadDummy);
            Assert.Equal(queryResponse.EntidadDummy.DummyC.Name, commandResponse.EntidadDummy.DummyC.Name); // Asegurar de que el ID sea el esperado
            Assert.True(queryResponse.EntidadDummy.DummiesB.Count == 1, "La lista de DummiesB no contiene el item."); // Asegurar de que el ID sea el esperado
        }

        [Fact]
        public async Task Delete_EntidadDummy()
        {
            // Arrange
            var entidadDummy = _service.GetEntidadDummy();

            // Act
            entidadDummy = (await _service.Create(entidadDummy)).EntidadDummy;

            var commandResponse = await _service.Delete(entidadDummy!.Id);

            var queryResponse = await _service.GetById(entidadDummy!.Id);

            // Assert
            Assert.True(queryResponse.EntidadDummy is null);
        }
        #endregion

        #region Queries
        [Fact]
        public async Task GetById_EntidadDummy()
        {
            // Arrange
            var entidadDummy = _service.GetEntidadDummy();
            entidadDummy = (await _service.Create(entidadDummy)).EntidadDummy;

            // Arrange y Act se hacen dentro del service.
            // Obtengo EntidadDummy con id = 2
            var response = await _service.GetById(entidadDummy!.Id);

            // Assert
            Assert.NotNull(response?.EntidadDummy);
            Assert.Equal(entidadDummy.Id, response.EntidadDummy.Id); // Asegurar de que el ID sea el esperado
        }

        [Fact]
        public async Task GetPaged_EntidadDummy()
        {
            // Arrange
            var entidadDummy = _service.GetEntidadDummy();
            var commandResponse = await _service.Create(entidadDummy);

            // Arrange y Act se hacen dentro del service.
            // Obtengo un listado de EntidadDummy paginado.
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

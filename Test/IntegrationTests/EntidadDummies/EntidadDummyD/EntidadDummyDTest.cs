using Microsoft.AspNetCore.Mvc.Testing;
using Shared.Portable.Enums.EntityState;
using Test.IntegrationTests.Base;

namespace Test.IntegrationTests.EntidadDummies
{
    public class EntidadDummyDTest : BaseTest, IDisposable
    {
        protected EntidadDummyDTestService _serviceEntidadDummyD;
        // Agrego este servicio para crear entidades del tipo EntidadDummy
        // y asi poder probar la relacion de muchos a muchos con EntidadDummyD, ya que,
        // son dos aggregate
        protected EntidadDummyTestService _serviceEntidadDummyA;
        public EntidadDummyDTest(WebApplicationFactory<Program> factory) 
        {
            _serviceEntidadDummyD = new(factory);
            _serviceEntidadDummyA = new(factory);
        }

        #region Commands
        [Fact]
        public async Task Create_EntidadDummyD()
        {
            // Arrange
            var EntidadDummyD = _serviceEntidadDummyD.GetEntidadDummyD();

            // Act
            var commandResponse = await _serviceEntidadDummyD.Create(EntidadDummyD);

            Assert.True(commandResponse.Success, commandResponse.StackTrace);

            var queryResponse = await _serviceEntidadDummyD.GetById(commandResponse.EntidadDummyD!.Id);

            // Assert
            Assert.NotNull(queryResponse.EntidadDummyD);
            Assert.Equal(queryResponse.EntidadDummyD.Id, commandResponse.EntidadDummyD.Id); // Asegurar de que el ID sea el esperado
        }

        /// <summary>
        /// Crea una EntidadDummyD con relacion N -> N con EntidadDummy
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Create_EntidadDummyDWithDummiesA()
        {
            // Arrange
            var EntidadDummyD = _serviceEntidadDummyD.GetEntidadDummyD();
            var EntidadDummy = _serviceEntidadDummyA.GetEntidadDummy();

            // Act
            var commandResponseDummyA = await _serviceEntidadDummyA.Create(EntidadDummy);
            EntidadDummy = (await _serviceEntidadDummyA.GetById(commandResponseDummyA.EntidadDummy!.Id)).EntidadDummy;


            EntidadDummyD.DummiesA.Add(commandResponseDummyA.EntidadDummy!);
            var commandResponseDummyD = await _serviceEntidadDummyD.Create(EntidadDummyD);

            var queryResponse = await _serviceEntidadDummyD.GetById(commandResponseDummyD.EntidadDummyD!.Id);

            // Assert
            Assert.NotNull(queryResponse.EntidadDummyD);
            Assert.Equal(queryResponse.EntidadDummyD.Id, commandResponseDummyD.EntidadDummyD.Id); // Asegurar de que el ID sea el esperado
        }

        [Fact]
        public async Task Update_EntidadDummyD()
        {
            // Arrange
            var EntidadDummyD = _serviceEntidadDummyD.GetEntidadDummyD();

            // Act
            EntidadDummyD = (await _serviceEntidadDummyD.Create(EntidadDummyD)).EntidadDummyD;

            // Marco la entidad como modificada aunque al IAggregateRoot no hace falta marcarlo (es a modo de ejemplo).
            EntidadDummyD!.EntityState = EntityStateMark.Modified;
            
          
            var commandResponse = await _serviceEntidadDummyD.Update(EntidadDummyD);

            var queryResponse = await _serviceEntidadDummyD.GetById(commandResponse.EntidadDummyD!.Id);

            // Assert
            Assert.NotNull(queryResponse.EntidadDummyD);
        }

        [Fact]
        public async Task Delete_EntidadDummyD()
        {
            // Arrange
            var EntidadDummyD = _serviceEntidadDummyD.GetEntidadDummyD();

            // Act
            EntidadDummyD = (await _serviceEntidadDummyD.Create(EntidadDummyD)).EntidadDummyD;

            var commandResponse = await _serviceEntidadDummyD.Delete(EntidadDummyD!.Id);

            var queryResponse = await _serviceEntidadDummyD.GetById(EntidadDummyD!.Id);

            // Assert
            Assert.True(queryResponse.EntidadDummyD is null);
        }
        #endregion

        #region Queries
        [Fact]
        public async Task GetById_EntidadDummyD()
        {
            // Arrange
            var entidadDummyD = _serviceEntidadDummyD.GetEntidadDummyD();

            // Act
            entidadDummyD = (await _serviceEntidadDummyD.Create(entidadDummyD)).EntidadDummyD;

            // Arrange y Act se hacen dentro del service.
            // Obtengo EntidadDummyD con id = 2
            var response = await _serviceEntidadDummyD.GetById(entidadDummyD.Id);

            // Assert
            Assert.NotNull(response?.EntidadDummyD);
            Assert.Equal(entidadDummyD.Id, response.EntidadDummyD.Id); // Asegurar de que el ID sea el esperado
        }

        [Fact]
        public async Task GetPaged_EntidadDummyD()
        {
            // Arrange
            var EntidadDummyD = _serviceEntidadDummyD.GetEntidadDummyD();

            // Act
            EntidadDummyD = (await _serviceEntidadDummyD.Create(EntidadDummyD)).EntidadDummyD;

            // Arrange y Act se hacen dentro del service.
            // Obtengo un listado de EntidadDummyD paginado.
            // Posee parametros opcionales para el numero de pagina y el tamaño de pagina.
            var response = await _serviceEntidadDummyD.GetPaged();

            // Assert
            Assert.True(response.Success, response.Message);
            Assert.True(response.PaginationData.Items.Any(), "La query para obtener los datos paginados no devolvió ningún item"); 
        }


        #endregion

        public void Dispose()
        {
            _serviceEntidadDummyD.Dispose();
            _serviceEntidadDummyA.Dispose();
        }
    }
}

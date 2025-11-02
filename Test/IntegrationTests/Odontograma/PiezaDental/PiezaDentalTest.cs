using Microsoft.AspNetCore.Mvc.Testing;
using Shared.Portable.Enums.EntityState;
using Test.IntegrationTests.Base;

namespace Test.IntegrationTests.Odontogramas
{
    public class PiezaDentalTest : BaseTest, IDisposable
    {
        protected PiezaDentalTestService _service;
        public PiezaDentalTest(WebApplicationFactory<Program> factory) 
        {
            _service = new(factory);
        }

        #region Commands
        [Fact]
        public async Task Create_PiezaDental()
        {
            // Arrange
            var PiezaDental = _service.GetPiezaDental();

            // Act
            var commandResponse = await _service.Create(PiezaDental);

            Assert.True(commandResponse.Success, commandResponse.StackTrace);

            var queryResponse = await _service.GetById(commandResponse.PiezaDental!.Id);

            // Assert
            Assert.NotNull(queryResponse.PiezaDental);
            Assert.Equal(queryResponse.PiezaDental.Id, commandResponse.PiezaDental.Id); // Asegurar de que el ID sea el esperado
        }

        [Fact]
        public async Task Update_PiezaDental()
        {
            // Arrange
            var PiezaDental = _service.GetPiezaDental();


            // Act
            PiezaDental = (await _service.Create(PiezaDental)).PiezaDental;

            // Marco la entidad como modificada aunque al IAggregateRoot no hace falta marcarlo (es a modo de ejemplo).
            PiezaDental!.EntityState = EntityStateMark.Modified;
            PiezaDental.NumeroPieza = 83;

            var commandResponse = await _service.Update(PiezaDental);
            Assert.True(commandResponse.Success, commandResponse.Message);

            var queryResponse = await _service.GetById(commandResponse.PiezaDental!.Id);

            // Assert
            Assert.NotNull(queryResponse.PiezaDental);
            Assert.Equal(queryResponse.PiezaDental.NumeroPieza, commandResponse.PiezaDental.NumeroPieza); // Asegurar de que el ID sea el esperado
        }

        [Fact]
        public async Task Delete_PiezaDental()
        {
            // Arrange
            var PiezaDental = _service.GetPiezaDental();

            // Act
            PiezaDental = (await _service.Create(PiezaDental)).PiezaDental;

            var commandResponse = await _service.Delete(PiezaDental!.Id);

            var queryResponse = await _service.GetById(PiezaDental!.Id);

            // Assert
            Assert.True(queryResponse.PiezaDental is null);
        }
        #endregion

        #region Queries
        [Fact]
        public async Task GetById_PiezaDental()
        {
            // Arrange
            var PiezaDental = _service.GetPiezaDental();
             PiezaDental = (await _service.Create(PiezaDental)).PiezaDental;

            // Arrange y Act se hacen dentro del service.
            // Obtengo PiezaDental con id = 2
            var response = await _service.GetById(PiezaDental!.Id);

            // Assert
            Assert.NotNull(response?.PiezaDental);
            Assert.Equal(PiezaDental.Id, response.PiezaDental.Id); // Asegurar de que el ID sea el esperado
        }

        [Fact]
        public async Task GetPaged_PiezaDental()
        {
            var PiezaDental = _service.GetPiezaDental();
            PiezaDental = (await _service.Create(PiezaDental)).PiezaDental;

            // Arrange y Act se hacen dentro del service.
            // Obtengo un listado de PiezaDental paginado.
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

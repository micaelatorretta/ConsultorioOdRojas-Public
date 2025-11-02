using Microsoft.AspNetCore.Mvc.Testing;
using Portable.Enums;
using Shared.Portable.Enums.EntityState;
using Test.IntegrationTests.Base;

namespace Test.IntegrationTests.Odontogramas
{
    public class CaraDentalTest : BaseTest, IDisposable
    {
        protected CaraDentalTestService _service;
        public CaraDentalTest(WebApplicationFactory<Program> factory) 
        {
            _service = new(factory);
        }

        #region Commands
        [Fact]
        public async Task Create_CaraDental()
        {
            // Arrange
            var CaraDental = _service.GetCaraDental();

            // Act
            var commandResponse = await _service.Create(CaraDental);

            Assert.True(commandResponse.Success, commandResponse.StackTrace);

            var queryResponse = await _service.GetById(commandResponse.CaraDental!.Id);

            // Assert
            Assert.NotNull(queryResponse.CaraDental);
            Assert.Equal(queryResponse.CaraDental.Id, commandResponse.CaraDental.Id); // Asegurar de que el ID sea el esperado
        }

        [Fact]
        public async Task Update_CaraDental()
        {
            // Arrange
            var CaraDental = _service.GetCaraDental();


            // Act
            CaraDental = (await _service.Create(CaraDental)).CaraDental;

            // Marco la entidad como modificada aunque al IAggregateRoot no hace falta marcarlo (es a modo de ejemplo).
            CaraDental!.EntityState = EntityStateMark.Modified;
            CaraDental.CaraDentaria = TipoCara.Oclusal;

            var commandResponse = await _service.Update(CaraDental);
            Assert.True(commandResponse.Success, commandResponse.Message);

            var queryResponse = await _service.GetById(commandResponse.CaraDental!.Id);

            // Assert
            Assert.NotNull(queryResponse.CaraDental);
            Assert.Equal(queryResponse.CaraDental.CaraDentaria, commandResponse.CaraDental.CaraDentaria); // Asegurar de que el ID sea el esperado
        }

        [Fact]
        public async Task Delete_CaraDental()
        {
            // Arrange
            var CaraDental = _service.GetCaraDental();

            // Act
            CaraDental = (await _service.Create(CaraDental)).CaraDental;

            var commandResponse = await _service.Delete(CaraDental!.Id);

            var queryResponse = await _service.GetById(CaraDental!.Id);

            // Assert
            Assert.True(queryResponse.CaraDental is null);
        }
        #endregion

        #region Queries
        [Fact]
        public async Task GetById_CaraDental()
        {
            // Arrange
            var CaraDental = _service.GetCaraDental();
             CaraDental = (await _service.Create(CaraDental)).CaraDental;

            // Arrange y Act se hacen dentro del service.
            // Obtengo CaraDental con id = 2
            var response = await _service.GetById(CaraDental!.Id);

            // Assert
            Assert.NotNull(response?.CaraDental);
            Assert.Equal(CaraDental.Id, response.CaraDental.Id); // Asegurar de que el ID sea el esperado
        }

        [Fact]
        public async Task GetPaged_CaraDental()
        {
            var CaraDental = _service.GetCaraDental();
            CaraDental = (await _service.Create(CaraDental)).CaraDental;

            // Arrange y Act se hacen dentro del service.
            // Obtengo un listado de CaraDental paginado.
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

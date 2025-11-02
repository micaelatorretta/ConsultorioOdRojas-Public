using Microsoft.AspNetCore.Mvc.Testing;
using Portable.Enums;
using Shared.Portable.Enums.EntityState;
using Test.IntegrationTests.Base;

namespace Test.IntegrationTests.Odontogramas
{
    public class OdontogramaTest : BaseTest, IDisposable
    {
        protected OdontogramaTestService _service;
        public OdontogramaTest(WebApplicationFactory<Program> factory) 
        {
            _service = new(factory);
        }

        #region Commands
        [Fact]
        public async Task Create_Odontograma()
        {
            // Arrange
            var Odontograma = _service.GetOdontograma();

            // Act
            var commandResponse = await _service.Create(Odontograma);

            Assert.True(commandResponse.Success, commandResponse.StackTrace);

            var queryResponse = await _service.GetById(commandResponse.Odontograma!.Id);

            // Assert
            Assert.NotNull(queryResponse.Odontograma);
            Assert.Equal(queryResponse.Odontograma.Id, commandResponse.Odontograma.Id); // Asegurar de que el ID sea el esperado
        }

        [Fact]
        public async Task Update_Odontograma()
        {
            // Arrange
            var Odontograma = _service.GetOdontograma();


            // Act
            Odontograma = (await _service.Create(Odontograma)).Odontograma;

            // Marco la entidad como modificada aunque al IAggregateRoot no hace falta marcarlo (es a modo de ejemplo).
            Odontograma!.EntityState = EntityStateMark.Modified;
         //   Odontograma.CaraDentaria = TipoCara.Oclusal;

            var commandResponse = await _service.Update(Odontograma);
            Assert.True(commandResponse.Success, commandResponse.Message);

            var queryResponse = await _service.GetById(commandResponse.Odontograma!.Id);

            // Assert
            Assert.NotNull(queryResponse.Odontograma);
         //   Assert.Equal(queryResponse.Odontograma.CaraDentaria, commandResponse.Odontograma.CaraDentaria); // Asegurar de que el ID sea el esperado
        }

        [Fact]
        public async Task Delete_Odontograma()
        {
            // Arrange
            var Odontograma = _service.GetOdontograma();

            // Act
            Odontograma = (await _service.Create(Odontograma)).Odontograma;

            var commandResponse = await _service.Delete(Odontograma!.Id);

            var queryResponse = await _service.GetById(Odontograma!.Id);

            // Assert
            Assert.True(queryResponse.Odontograma is null);
        }
        #endregion

        #region Queries
        [Fact]
        public async Task GetById_Odontograma()
        {
            // Arrange
            var Odontograma = _service.GetOdontograma();
             Odontograma = (await _service.Create(Odontograma)).Odontograma;

            // Arrange y Act se hacen dentro del service.
            // Obtengo Odontograma con id = 2
            var response = await _service.GetById(Odontograma!.Id);

            // Assert
            Assert.NotNull(response?.Odontograma);
            Assert.Equal(Odontograma.Id, response.Odontograma.Id); // Asegurar de que el ID sea el esperado
        }

        [Fact]
        public async Task GetPaged_Odontograma()
        {
            var Odontograma = _service.GetOdontograma();
            Odontograma = (await _service.Create(Odontograma)).Odontograma;

            // Arrange y Act se hacen dentro del service.
            // Obtengo un listado de Odontograma paginado.
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

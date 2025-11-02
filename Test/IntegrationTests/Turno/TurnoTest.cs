using Microsoft.AspNetCore.Mvc.Testing;
using Shared.Portable.Enums.EntityState;
using Test.IntegrationTests.Base;

namespace Test.IntegrationTests.Turnos
{
    public class TurnoTest : BaseTest, IDisposable
    {
        protected TurnoTestService _service;
        public TurnoTest(WebApplicationFactory<Program> factory) 
        {
            _service = new(factory);
        }

        #region Commands
        [Fact]
        public async Task Create_Turno()
        {
            // Arrange
            var Turno = _service.GetTurno();

            // Act
            var commandResponse = await _service.Create(Turno);

            Assert.True(commandResponse.Success, commandResponse.StackTrace);

            var queryResponse = await _service.GetById(commandResponse.Turno!.Id);

            // Assert
            Assert.NotNull(queryResponse.Turno);
            Assert.Equal(queryResponse.Turno.Id, commandResponse.Turno.Id); // Asegurar de que el ID sea el esperado
        }

        [Fact]
        public async Task Update_Turno()
        {
            // Arrange
            var Turno = _service.GetTurno();

            // Act
            Turno = (await _service.Create(Turno)).Turno;

            // Marco la entidad como modificada aunque al IAggregateRoot no hace falta marcarlo (es a modo de ejemplo).
            Turno!.EntityState = EntityStateMark.Modified;
            Turno.Fecha = DateOnly.FromDateTime(DateTime.Now.AddDays(1));

            var commandResponse = await _service.Update(Turno);
            Assert.True(commandResponse.Success, commandResponse.Message);

            var queryResponse = await _service.GetById(commandResponse.Turno!.Id);

            // Assert
            Assert.NotNull(queryResponse.Turno);
            Assert.Equal(queryResponse.Turno.Fecha.Day, commandResponse.Turno.Fecha.Day); // Asegurar de que el ID sea el esperado
        }

        [Fact]
        public async Task Delete_Turno()
        {
            // Arrange
            var Turno = _service.GetTurno();

            // Act
            Turno = (await _service.Create(Turno)).Turno;

            var commandResponse = await _service.Delete(Turno!.Id);

            var queryResponse = await _service.GetById(Turno!.Id);

            // Assert
            Assert.True(queryResponse.Turno is null);
        }
        #endregion

        #region Queries
        [Fact]
        public async Task GetById_Turno()
        {
            // Arrange
            var turno = _service.GetTurno();

            // Act
            turno = (await _service.Create(turno)).Turno;

            // Arrange y Act se hacen dentro del service.
            // Obtengo Turno con id = 2
            var response = await _service.GetById(turno!.Id);

            // Assert
            Assert.NotNull(response?.Turno);
            Assert.Equal(turno.Id, response.Turno.Id); // Asegurar de que el ID sea el esperado
        }

        [Fact]
        public async Task GetPaged_Turno()
        {
            // Arrange y Act se hacen dentro del service.
            // Obtengo un listado de Turno paginado.
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

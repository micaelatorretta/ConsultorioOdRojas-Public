using Microsoft.AspNetCore.Mvc.Testing;
using Shared.Portable.Enums.EntityState;
using Shared.Portable.Enums.Filters;
using Shared.Portable.Filters;
using Shared.Portable.FiltersAndSort;
using Shared.Portable.Pagination;
using Test.IntegrationTests.Base;

namespace Test.IntegrationTests.Pacientes
{
    public class PacienteTest : BaseTest, IDisposable
    {
        protected PacienteTestService _service;
        public PacienteTest(WebApplicationFactory<Program> factory) 
        {
            _service = new(factory);
        }

        #region Commands
        [Fact]
        public async Task Create_Paciente()
        {
            // Arrange
            var Paciente = _service.GetPaciente();

            // Act
            var commandResponse = await _service.Create(Paciente);

            Assert.True(commandResponse.Success, commandResponse.StackTrace);

            var queryResponse = await _service.GetById(commandResponse.Paciente!.Id);

            // Assert
            Assert.NotNull(queryResponse.Paciente);
            Assert.Equal(queryResponse.Paciente.Id, commandResponse.Paciente.Id); // Asegurar de que el ID sea el esperado
        }

        [Fact]
        public async Task Update_Paciente()
        {
            // Arrange
            var Paciente = _service.GetPaciente();

            // Act
            Paciente = (await _service.Create(Paciente)).Paciente;

            // Marco la entidad como modificada aunque al IAggregateRoot no hace falta marcarlo (es a modo de ejemplo).
            Paciente!.EntityState = EntityStateMark.Modified;
            Paciente.Nombre = "Nombre modificado";

            var commandResponse = await _service.Update(Paciente);
            Assert.True(commandResponse.Success, commandResponse.Message);

            var queryResponse = await _service.GetById(commandResponse.Paciente!.Id);

            // Assert
            Assert.NotNull(queryResponse.Paciente);
            Assert.Equal(queryResponse.Paciente.Nombre, commandResponse.Paciente.Nombre); // Asegurar de que el ID sea el esperado
        }

        [Fact]
        public async Task Delete_Paciente()
        {
            // Arrange
            var Paciente = _service.GetPaciente();

            // Act
            Paciente = (await _service.Create(Paciente)).Paciente;

            var commandResponse = await _service.Delete(Paciente!.Id);

            var queryResponse = await _service.GetById(Paciente!.Id);

            // Assert
            Assert.True(queryResponse.Paciente is null);
        }
        #endregion

        #region Queries
        [Fact]
        public async Task GetById_Paciente()
        {
            // Arrange
            var Paciente = _service.GetPaciente();

            // Act
            Paciente = (await _service.Create(Paciente)).Paciente;

            // Arrange y Act se hacen dentro del service.
            // Obtengo Paciente con id = 2
            var response = await _service.GetById(Paciente!.Id);

            // Assert
            Assert.NotNull(response?.Paciente);
            Assert.Equal(Paciente.Id, response.Paciente.Id); // Asegurar de que el ID sea el esperado
        }

        [Fact]
        public async Task GetPaged_Paciente()
        {
            // Arrange
            var Paciente = _service.GetPaciente();

            var commandResponse = await _service.Create(Paciente);

            // Arrange y Act se hacen dentro del service.
            // Obtengo un listado de Paciente paginado.
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

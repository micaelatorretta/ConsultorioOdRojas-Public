using Microsoft.AspNetCore.Mvc.Testing;
using Shared.Portable.Enums.EntityState;
using Shared.Portable.Enums.Filters;
using Shared.Portable.Filters;
using Shared.Portable.FiltersAndSort;
using Shared.Portable.Pagination;
using Test.IntegrationTests.Base;

namespace Test.IntegrationTests.Usuarios
{
    public class UsuarioTest : BaseTest, IDisposable
    {
        protected UsuarioTestService _service;
        public UsuarioTest(WebApplicationFactory<Program> factory) 
        {
            _service = new(factory);
        }

        #region Commands
        [Fact]
        public async Task Create_Usuario()
        {
            // Arrange
            var Usuario = _service.GetUsuario();

            // Act
            var commandResponse = await _service.Create(Usuario);

            Assert.True(commandResponse.Success, commandResponse.StackTrace);

            var queryResponse = await _service.GetById(commandResponse.Usuario!.Id);

            // Assert
            Assert.NotNull(queryResponse.Usuario);
            Assert.Equal(queryResponse.Usuario.Id, commandResponse.Usuario.Id); // Asegurar de que el ID sea el esperado
        }

        [Fact]
        public async Task Update_Usuario()
        {
            // Arrange
            var Usuario = _service.GetUsuario();

            // Act
            Usuario = (await _service.Create(Usuario)).Usuario;

            // Marco la entidad como modificada aunque al IAggregateRoot no hace falta marcarlo (es a modo de ejemplo).
            Usuario!.EntityState = EntityStateMark.Modified;
            Usuario.Nombre = "Nombre modificado";

            var commandResponse = await _service.Update(Usuario);
            Assert.True(commandResponse.Success, commandResponse.Message);

            var queryResponse = await _service.GetById(commandResponse.Usuario!.Id);

            // Assert
            Assert.NotNull(queryResponse.Usuario);
            Assert.Equal(queryResponse.Usuario.Nombre, commandResponse.Usuario.Nombre); // Asegurar de que el ID sea el esperado
        }

        [Fact]
        public async Task Delete_Usuario()
        {
            // Arrange
            var Usuario = _service.GetUsuario();

            // Act
            Usuario = (await _service.Create(Usuario)).Usuario;

            var commandResponse = await _service.Delete(Usuario!.Id);

            var queryResponse = await _service.GetById(Usuario!.Id);

            // Assert
            Assert.True(queryResponse.Usuario is null);
        }
        #endregion

        #region Queries
        [Fact]
        public async Task GetById_Usuario()
        {
            // Arrange
            var usuario = _service.GetUsuario();

            // Act
            usuario = (await _service.Create(usuario)).Usuario;

            // Arrange y Act se hacen dentro del service.
            // Obtengo Usuario con id = 2
            var response = await _service.GetById(usuario!.Id);

            // Assert
            Assert.NotNull(response?.Usuario);
            Assert.Equal(usuario.Id, response.Usuario.Id); // Asegurar de que el ID sea el esperado
        }

        [Fact]
        public async Task GetPaged_Usuario()
        {
            // Arrange
            var Usuario = _service.GetUsuario();

            var commandResponse = await _service.Create(Usuario);

            // Arrange y Act se hacen dentro del service.
            // Obtengo un listado de Usuario paginado.
            // Posee parametros opcionales para el numero de pagina y el tamaño de pagina.
            var response = await _service.GetPaged();

            // Assert
            Assert.True(response.Success, response.Message);
            Assert.True(response.PaginationData.Items.Any(), "La query para obtener los datos paginados no devolvió ningún item"); 
        }

        #region Paginacion con Filtros y Ordenamiento
        [Fact]
        public async Task GetPaged_Usuario_WithFiltersAndSorting()
        {
            // Arrange
            var usuario1 = _service.GetUsuario();
            usuario1.Nombre = "Carlos";

            var usuario2 = _service.GetUsuario();
            usuario2.Nombre = "Ana";

            await _service.Create(usuario1);
            await _service.Create(usuario2);

            // Definir filtros: Buscar usuarios con nombre "Carlos"
            var filters = new List<FieldWhereDefinition>
            {
                new FieldWhereDefinition() { PropertyName = "Nombre", Value = "Carlos", IsGroup=false, Condition=FilterOperator.Equals }
            };

            // Definir ordenamiento: Ordenar por FechaCreacion descendente
            var sorts = new List<SortDefinition>
            {
                new SortDefinition() { PropertyName = "CreatedDate", Direction = SortDirection.Descending }
            };

            // Act
            var response = await _service.GetPaged(1, PaginationPageSize.Default, filters, sorts);

            // Assert
            Assert.True(response.Success, response.Message);
            Assert.True(response.PaginationData.Items.Any(), "La query para obtener los datos paginados no devolvió ningún item");
            Assert.All(response.PaginationData.Items, u => Assert.Equal("Carlos", u.Nombre)); // Verificar filtro

        }

        /// <summary>
        /// Resultado del filtro:
        ///        SELECT*
        ///        FROM Usuarios
        ///     WHERE
        ///    ((Nombre = 'Carlos' AND Apellido = 'Sosa')
        ///      OR
        ///      (Nombre = 'Roberto' AND Apellido = 'Sosa') )
        /// ORDER BY Apellido ASC, Nombre ASC;
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetPaged_Usuario_WithAdvancedFiltersAndSorting()
        {
            // Arrange: Crear usuarios con diferentes combinaciones de nombres y apellidos
            var usuario1 = _service.GetUsuario();
            usuario1.Nombre = "Carlos";
            usuario1.Apellido = "Sosa";
            usuario1.Email = "carlos.sosa@example.com";

            var usuario2 = _service.GetUsuario();
            usuario2.Nombre = "Ana";
            usuario2.Apellido = "Gomez";
            usuario2.Email = "ana.gomez@example.com";

            var usuario3 = _service.GetUsuario();
            usuario3.Nombre = "Roberto";
            usuario3.Apellido = "Sosa";
            usuario3.Email = "roberto.sosa@example.com";

            var usuario4 = _service.GetUsuario();
            usuario4.Nombre = "Juan";
            usuario4.Apellido = "Perez";
            usuario4.Email = "juan.perez@example.com";

            await _service.Create(usuario1);
            await _service.Create(usuario2);
            await _service.Create(usuario3);
            await _service.Create(usuario4);

            // Definir filtros:
            // ((Nombre == "Carlos" AND Apellido == "Sosa") OR (Nombre == "Roberto" AND Apellido == "Sosa"))
            var filters = new List<FieldWhereDefinition>
            {
                new FieldWhereDefinition
                {
                    IsAnd = false, // OR entre los grupos
                    IsGroup = true, // Grupo de condiciones
                    SubConditions = new List<FieldWhereDefinition>
                    {
                        new FieldWhereDefinition
                        {
                            IsAnd = true, // (Nombre == "Carlos" AND Apellido == "Sosa")
                            IsGroup = true,
                            SubConditions = new List<FieldWhereDefinition>
                            {
                                new FieldWhereDefinition { PropertyName = "Nombre", Value = "Carlos", Condition = FilterOperator.Equals },
                                new FieldWhereDefinition { PropertyName = "Apellido", Value = "Sosa", Condition = FilterOperator.Equals }
                            }
                        },
                        new FieldWhereDefinition
                        {
                            IsAnd = true, // (Nombre == "Roberto" AND Apellido == "Sosa")
                            IsGroup = true,
                            SubConditions = new List<FieldWhereDefinition>
                            {
                                new FieldWhereDefinition { PropertyName = "Nombre", Value = "Roberto", Condition = FilterOperator.Equals },
                                new FieldWhereDefinition { PropertyName = "Apellido", Value = "Sosa", Condition = FilterOperator.Equals }
                            }
                        }
                    }
                }
            };

            // Definir ordenamiento: Ordenar por Apellido ascendente, luego por Nombre ascendente
            var sorts = new List<SortDefinition>
            {
                new SortDefinition { PropertyName = "Apellido", Direction = SortDirection.Ascending },
                new SortDefinition { PropertyName = "Nombre", Direction = SortDirection.Ascending }
            };

            // Act
            var response = await _service.GetPaged(1, PaginationPageSize.Default, filters, sorts);

            // Assert
            Assert.True(response.Success, response.Message);
            Assert.True(response.PaginationData.Items.Any(), "La query para obtener los datos paginados no devolvió ningún item");

            // Verificar que solo se obtienen usuarios "Carlos Sosa" y "Roberto Sosa"
            Assert.All(response.PaginationData.Items, u =>
            {
                bool cumpleFiltro =
                    (u.Nombre == "Carlos" && u.Apellido == "Sosa") ||
                    (u.Nombre == "Roberto" && u.Apellido == "Sosa");
                Assert.True(cumpleFiltro, $"El usuario {u.Nombre} {u.Apellido} no cumple con los filtros.");
            });

            // Verificar ordenamiento (Apellido ascendente, luego Nombre ascendente)
            var orderedUsers = response.PaginationData.Items
                .OrderBy(u => u.Apellido)
                .ThenBy(u => u.Nombre)
                .ToList();

            Assert.Equal(orderedUsers, response.PaginationData.Items);
        }

        #endregion

        #endregion

        public void Dispose()
        {
            _service.Dispose();
        }
    }
}

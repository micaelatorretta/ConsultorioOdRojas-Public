using Microsoft.AspNetCore.Mvc.Testing;
using Portable.Enums.Auth;
using Portable.FunctionalUnits.Usuarios.Commands;
using Portable.FunctionalUnits.Usuarios.DTOs;
using Portable.FunctionalUnits.Usuarios.Queries;
using Portable.FunctionalUnits.Usuarios.Responses;
using Shared.Portable.Filters;
using Shared.Portable.FiltersAndSort;
using Shared.Portable.Pagination;
using Test.IntegrationTests.Base;

namespace Test.IntegrationTests.Usuarios
{
    public class UsuarioTestService : BaseTestService, IDisposable
    {
        public readonly string URL_GET_BY_ID = "/Usuarios/GetUsuarioById";
        public readonly string URL_GET_PAGED = "/Usuarios/GetUsuarioPaged";
        private const string URL_CREATE = "/Usuarios/CreateUsuario";
        private const string URL_UPDATE = "/Usuarios/UpdateUsuario";
        private const string URL_DELETE = "/Usuarios/DeleteUsuario";

        public UsuarioTestService(WebApplicationFactory<Program> factory) : base(factory)
        {
        }

        #region Actions

        #region Commands
        public async Task<CreateUsuarioResponse> Create(UsuarioDTO Usuario)
        {
            var command = new CreateUsuarioCommand
            {
                Usuario = Usuario
            };

            var response = await SendRequestAndDeserializeResponse<CreateUsuarioCommand, CreateUsuarioResponse>(URL_CREATE, command);

            // Se agrega a la lista cleanUpList para borrar la entidad una vez terminado el test.
            if (cleanUp && response.Success) cleanUpList.Add(response.Usuario!.Id);
            
            return response;
        }

        public async Task<UpdateUsuarioResponse> Update(UsuarioDTO Usuario)
        {
            var command = new UpdateUsuarioCommand
            {
                Usuario = Usuario
            };

            return await SendRequestAndDeserializeResponse<UpdateUsuarioCommand, UpdateUsuarioResponse>(URL_UPDATE, command);
        }

        public async Task<DeleteUsuarioResponse> Delete(int id)
        {
            var command = new DeleteUsuarioCommand
            {
                Id = id
            };

            return await SendRequestAndDeserializeResponse<DeleteUsuarioCommand, DeleteUsuarioResponse>(URL_DELETE, command);
        }
        #endregion

        #region Queries
        public async Task<GetUsuarioByIdResponse> GetById(int id)
        {
            // Arrange
            var query = new GetUsuarioByIdQuery { Id = id };

            // Act
            var response = await SendRequestAndDeserializeResponse<GetUsuarioByIdQuery, GetUsuarioByIdResponse>(URL_GET_BY_ID, query);

            return response;
        }

        public async Task<GetUsuarioPagedResponse> GetPaged(int pageNumber = 1, 
            PaginationPageSize pageSize = PaginationPageSize.Default, 
            List<FieldWhereDefinition>? filters = null, 
            List<SortDefinition>? sorts = null)
        {
            // Arrange
            var paginationParams = new PaginationParams()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Filters = filters,
                Sorts = sorts
            };
            var query = new GetUsuarioPagedQuery(paginationParams);

            // Act
            var response = await SendRequestAndDeserializeResponse<GetUsuarioPagedQuery, GetUsuarioPagedResponse>(URL_GET_PAGED, query);

            return response;
        }
        #endregion

        #endregion

        #region Getters
        public UsuarioDTO GetUsuario(string? nombre = null, 
                                     string? apellido = null, 
                                     string? email = null, 
                                     string? password = null, 
                                     UsuarioRol? rol = null,
                                     string? username = null)
        {
            var Usuario = new UsuarioDTO()
            {
                Nombre = nombre ?? "Nombre de Prueba",
                Apellido = apellido ?? "Apellido de Prueba",
                Email = email ?? "email@prueba.com",
                Password = password ?? "password123",
                Rol = rol ?? UsuarioRol.Secretaria,
                Username = username ?? RandomGenerator.GenerateRandomString(10)
            };

            return Usuario;
        }



        #endregion


        public void Dispose()
        {
            if (cleanUp)
            {
                cleanUpList.ForEach(i => Delete(i).Wait());
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc.Testing;
using Portable.Enums;
using Portable.FunctionalUnits.EntidadDummies.Commands;
using Portable.FunctionalUnits.EntidadDummies.DTOs;
using Portable.FunctionalUnits.EntidadDummies.Queries;
using Portable.FunctionalUnits.EntidadDummies.Responses;
using Portable.ValueObjectsDTO;
using Shared.Portable.Enums.EntityState;
using Shared.Portable.Pagination;
using Test.IntegrationTests.Base;

namespace Test.IntegrationTests.EntidadDummies
{
    public class EntidadDummyTestService : BaseTestService, IDisposable
    {
        public readonly string URL_GET_BY_ID = "/EntidadDummies/GetEntidadDummyById";
        public readonly string URL_GET_PAGED = "/EntidadDummies/GetEntidadDummyPaged";
        private const string URL_CREATE = "/EntidadDummies/CreateEntidadDummy";
        private const string URL_UPDATE = "/EntidadDummies/UpdateEntidadDummy";
        private const string URL_DELETE = "/EntidadDummies/DeleteEntidadDummy";

        public EntidadDummyTestService(WebApplicationFactory<Program> factory) : base(factory)
        {
        }

        #region Actions

        #region Commands
        public async Task<CreateEntidadDummyResponse> Create(EntidadDummyDTO entidadDummy)
        {
            var command = new CreateEntidadDummyCommand
            {
                EntidadDummy = entidadDummy
            };

            var response = await SendRequestAndDeserializeResponse<CreateEntidadDummyCommand, CreateEntidadDummyResponse>(URL_CREATE, command);

            // Se agrega a la lista cleanUpList para borrar la entidad una vez terminado el test.
            if (cleanUp && response.Success) cleanUpList.Add(response.EntidadDummy!.Id);
            
            return response;
        }

        public async Task<UpdateEntidadDummyResponse> Update(EntidadDummyDTO entidadDummy)
        {
            var command = new UpdateEntidadDummyCommand
            {
                EntidadDummy = entidadDummy
            };

            return await SendRequestAndDeserializeResponse<UpdateEntidadDummyCommand, UpdateEntidadDummyResponse>(URL_UPDATE, command);
        }

        public async Task<DeleteEntidadDummyResponse> Delete(int id)
        {
            var command = new DeleteEntidadDummyCommand
            {
                Id = id
            };

            return await SendRequestAndDeserializeResponse<DeleteEntidadDummyCommand, DeleteEntidadDummyResponse>(URL_DELETE, command);
        }
        #endregion

        #region Queries
        public async Task<GetEntidadDummyByIdResponse> GetById(int id)
        {
            // Arrange
            var query = new GetEntidadDummyByIdQuery { Id = id };

            // Act
            var response = await SendRequestAndDeserializeResponse<GetEntidadDummyByIdQuery, GetEntidadDummyByIdResponse>(URL_GET_BY_ID, query);

            return response;
        }

        public async Task<GetEntidadDummyPagedResponse> GetPaged(int pageNumber = 1, PaginationPageSize pageSize = PaginationPageSize.Default)
        {
            // Arrange
            var paginationParams = new PaginationParams()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var query = new GetEntidadDummyPagedQuery(paginationParams);

            // Act
            var response = await SendRequestAndDeserializeResponse<GetEntidadDummyPagedQuery, GetEntidadDummyPagedResponse>(URL_GET_PAGED, query);

            return response;
        }
        #endregion

        #endregion

        #region Getters
        public EntidadDummyDTO GetEntidadDummy(string? name = null, EntidadDummyCDTO? entidadDummyC = null, ContactoTelefonicoDTO? contacto = null)
        {
            var entidadDummy = new EntidadDummyDTO() 
            { 
                Name = name ?? "EntidadDummy de Prueba",
                DummyC = entidadDummyC ?? GetEntidadDummyC(),
                Contacto = contacto ?? GetContactoTelefonico()
            };

            return entidadDummy;
        }

        public ContactoTelefonicoDTO GetContactoTelefonico(string? numero = null, TipoTelefono? tipo = null, string? extension=null)
        {
            var entidadDummy = new ContactoTelefonicoDTO(numero ?? "5336067", tipo ?? TipoTelefono.Celular, extension ?? "343");

            return entidadDummy;
        }

        public EntidadDummyCDTO GetEntidadDummyC(string? name = null)
        {
            var entidadDummy = new EntidadDummyCDTO() { Name = name ?? "Dummy C de Prueba" };

            return entidadDummy;
        }

        public EntidadDummyBDTO GetEntidadDummyB(string? name = null)
        {
            var entidadDummy = new EntidadDummyBDTO()
            { 
                Name = name ?? "Dummy B de Prueba",
                EntityState = EntityStateMark.Added
            };

            return entidadDummy;
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

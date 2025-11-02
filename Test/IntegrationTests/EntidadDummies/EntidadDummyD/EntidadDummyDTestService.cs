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
    public class EntidadDummyDTestService : BaseTestService, IDisposable
    {
        public readonly string URL_GET_BY_ID = "/EntidadDummies/GetEntidadDummyDById";
        public readonly string URL_GET_PAGED = "/EntidadDummies/GetEntidadDummyDPaged";
        private const string URL_CREATE = "/EntidadDummies/CreateEntidadDummyD";
        private const string URL_UPDATE = "/EntidadDummies/UpdateEntidadDummyD";
        private const string URL_DELETE = "/EntidadDummies/DeleteEntidadDummyD";

        public EntidadDummyDTestService(WebApplicationFactory<Program> factory) : base(factory)
        {
        }

        #region Actions

        #region Commands
        public async Task<CreateEntidadDummyDResponse> Create(EntidadDummyDDTO EntidadDummyD)
        {
            var command = new CreateEntidadDummyDCommand
            {
                EntidadDummyD = EntidadDummyD
            };

            var response = await SendRequestAndDeserializeResponse<CreateEntidadDummyDCommand, CreateEntidadDummyDResponse>(URL_CREATE, command);

            // Se agrega a la lista cleanUpList para borrar la entidad una vez terminado el test.
            if (cleanUp && response.Success) cleanUpList.Add(response.EntidadDummyD!.Id);
            
            return response;
        }

        public async Task<UpdateEntidadDummyDResponse> Update(EntidadDummyDDTO EntidadDummyD)
        {
            var command = new UpdateEntidadDummyDCommand
            {
                EntidadDummyD = EntidadDummyD
            };

            return await SendRequestAndDeserializeResponse<UpdateEntidadDummyDCommand, UpdateEntidadDummyDResponse>(URL_UPDATE, command);
        }

        public async Task<DeleteEntidadDummyDResponse> Delete(int id)
        {
            var command = new DeleteEntidadDummyDCommand
            {
                Id = id
            };

            return await SendRequestAndDeserializeResponse<DeleteEntidadDummyDCommand, DeleteEntidadDummyDResponse>(URL_DELETE, command);
        }
        #endregion

        #region Queries
        public async Task<GetEntidadDummyDByIdResponse> GetById(int id)
        {
            // Arrange
            var query = new GetEntidadDummyDByIdQuery { Id = id };

            // Act
            var response = await SendRequestAndDeserializeResponse<GetEntidadDummyDByIdQuery, GetEntidadDummyDByIdResponse>(URL_GET_BY_ID, query);

            return response;
        }

        public async Task<GetEntidadDummyDPagedResponse> GetPaged(int pageNumber = 1, PaginationPageSize pageSize = PaginationPageSize.Default)
        {
            // Arrange
            var paginationParams = new PaginationParams()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var query = new GetEntidadDummyDPagedQuery(paginationParams);

            // Act
            var response = await SendRequestAndDeserializeResponse<GetEntidadDummyDPagedQuery, GetEntidadDummyDPagedResponse>(URL_GET_PAGED, query);

            return response;
        }
        #endregion

        #endregion

        #region Getters
        public EntidadDummyDDTO GetEntidadDummyD(string? name = null)
        {
            var EntidadDummyD = new EntidadDummyDDTO() 
            { 
                Name = name ?? "EntidadDummyD de Prueba"
            };

            return EntidadDummyD;
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

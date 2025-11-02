using Microsoft.AspNetCore.Mvc.Testing;
using Portable.FunctionalUnits.ObrasSociales.Commands;
using Portable.FunctionalUnits.ObrasSociales.DTOs;
using Portable.FunctionalUnits.ObrasSociales.Queries;
using Portable.FunctionalUnits.ObrasSociales.Responses;
using Shared.Portable.Pagination;
using Test.IntegrationTests.Base;

namespace Test.IntegrationTests.ObrasSociales
{
    public class ObraSocialTestService : BaseTestService, IDisposable
    {
        public readonly string URL_GET_BY_ID = "/ObrasSociales/GetObraSocialById";
        public readonly string URL_GET_PAGED = "/ObrasSociales/GetObraSocialPaged";
        private const string URL_CREATE = "/ObrasSociales/CreateObraSocial";
        private const string URL_UPDATE = "/ObrasSociales/UpdateObraSocial";
        private const string URL_DELETE = "/ObrasSociales/DeleteObraSocial";

        public ObraSocialTestService(WebApplicationFactory<Program> factory) : base(factory)
        {
        }

        #region Actions

        #region Commands
        public async Task<CreateObraSocialResponse> Create(ObraSocialDTO ObraSocial)
        {
            var command = new CreateObraSocialCommand
            {
                ObraSocial = ObraSocial
            };

            var response = await SendRequestAndDeserializeResponse<CreateObraSocialCommand, CreateObraSocialResponse>(URL_CREATE, command);

            // Se agrega a la lista cleanUpList para borrar la entidad una vez terminado el test.
            if (cleanUp && response.Success) cleanUpList.Add(response.ObraSocial!.Id);
            
            return response;
        }

        public async Task<UpdateObraSocialResponse> Update(ObraSocialDTO ObraSocial)
        {
            var command = new UpdateObraSocialCommand
            {
                ObraSocial = ObraSocial
            };

            return await SendRequestAndDeserializeResponse<UpdateObraSocialCommand, UpdateObraSocialResponse>(URL_UPDATE, command);
        }

        public async Task<DeleteObraSocialResponse> Delete(int id)
        {
            var command = new DeleteObraSocialCommand
            {
                Id = id
            };

            return await SendRequestAndDeserializeResponse<DeleteObraSocialCommand, DeleteObraSocialResponse>(URL_DELETE, command);
        }
        #endregion

        #region Queries
        public async Task<GetObraSocialByIdResponse> GetById(int id)
        {
            // Arrange
            var query = new GetObraSocialByIdQuery { Id = id };

            // Act
            var response = await SendRequestAndDeserializeResponse<GetObraSocialByIdQuery, GetObraSocialByIdResponse>(URL_GET_BY_ID, query);

            return response;
        }

        public async Task<GetObraSocialPagedResponse> GetPaged(int pageNumber = 1, PaginationPageSize pageSize = PaginationPageSize.Default)
        {
            // Arrange
            var paginationParams = new PaginationParams()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var query = new GetObraSocialPagedQuery(paginationParams);

            // Act
            var response = await SendRequestAndDeserializeResponse<GetObraSocialPagedQuery, GetObraSocialPagedResponse>(URL_GET_PAGED, query);

            return response;
        }
        #endregion

        #endregion

        #region Getters
        public ObraSocialDTO GetObraSocial(string? nombre = null, string? codigo = null)
        {
            var ObraSocial = new ObraSocialDTO() 
            { 
                Nombre = nombre ?? "ObraSocial de Prueba",
                Codigo = codigo ?? "Codigo de Prueba"
            };

            return ObraSocial;
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

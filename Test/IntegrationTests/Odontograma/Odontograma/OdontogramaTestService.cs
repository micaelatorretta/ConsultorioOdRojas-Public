using Microsoft.AspNetCore.Mvc.Testing;
using Portable.Enums;
using Portable.FunctionalUnits.Odontogramas.Commands;
using Portable.FunctionalUnits.Odontogramas.DTOs;
using Portable.FunctionalUnits.Odontogramas.Queries;
using Portable.FunctionalUnits.Odontogramas.Responses;
using Shared.Portable.Pagination;
using Test.IntegrationTests.Base;

namespace Test.IntegrationTests.Odontogramas
{
    public class OdontogramaTestService : BaseTestService, IDisposable
    {
        public readonly string URL_GET_BY_ID = "/Odontogramas/GetOdontogramaById";
        public readonly string URL_GET_PAGED = "/Odontogramas/GetOdontogramaPaged";
        private const string URL_CREATE = "/Odontogramas/CreateOdontograma";
        private const string URL_UPDATE = "/Odontogramas/UpdateOdontograma";
        private const string URL_DELETE = "/Odontogramas/DeleteOdontograma";

        public OdontogramaTestService(WebApplicationFactory<Program> factory) : base(factory)
        {
            cleanUp = false;
        }

        #region Actions

        #region Commands
        public async Task<CreateOdontogramaResponse> Create(OdontogramaDTO Odontograma)
        {
            var command = new CreateOdontogramaCommand
            {
                Odontograma = Odontograma
            };

            var response = await SendRequestAndDeserializeResponse<CreateOdontogramaCommand, CreateOdontogramaResponse>(URL_CREATE, command);

            // Se agrega a la lista cleanUpList para borrar la entidad una vez terminado el test.
            if (cleanUp && response.Success) cleanUpList.Add(response.Odontograma!.Id);
            
            return response;
        }

        public async Task<UpdateOdontogramaResponse> Update(OdontogramaDTO Odontograma)
        {
            var command = new UpdateOdontogramaCommand
            {
                Odontograma = Odontograma
            };

            return await SendRequestAndDeserializeResponse<UpdateOdontogramaCommand, UpdateOdontogramaResponse>(URL_UPDATE, command);
        }

        public async Task<DeleteOdontogramaResponse> Delete(int id)
        {
            var command = new DeleteOdontogramaCommand
            {
                Id = id
            };

            return await SendRequestAndDeserializeResponse<DeleteOdontogramaCommand, DeleteOdontogramaResponse>(URL_DELETE, command);
        }
        #endregion

        #region Queries
        public async Task<GetOdontogramaByIdResponse> GetById(int id)
        {
            // Arrange
            var query = new GetOdontogramaByIdQuery { Id = id };

            // Act
            var response = await SendRequestAndDeserializeResponse<GetOdontogramaByIdQuery, GetOdontogramaByIdResponse>(URL_GET_BY_ID, query);

            return response;
        }

        public async Task<GetOdontogramaPagedResponse> GetPaged(int pageNumber = 1, PaginationPageSize pageSize = PaginationPageSize.Default)
        {
            // Arrange
            var paginationParams = new PaginationParams()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var query = new GetOdontogramaPagedQuery(paginationParams);

            // Act
            var response = await SendRequestAndDeserializeResponse<GetOdontogramaPagedQuery, GetOdontogramaPagedResponse>(URL_GET_PAGED, query);

            return response;
        }
        #endregion

        #endregion

        #region Getters
        public OdontogramaDTO GetOdontograma(/*TipoCara caraDentaria = TipoCara.Mesial*/)
        {
            var Odontograma = new OdontogramaDTO() 
            {
                //CaraDentaria = caraDentaria,
            };

            return Odontograma;
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

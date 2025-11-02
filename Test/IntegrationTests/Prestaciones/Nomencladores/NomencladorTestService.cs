using Microsoft.AspNetCore.Mvc.Testing;
using Portable.Enums;
using Portable.FunctionalUnits.Prestaciones.Commands;
using Portable.FunctionalUnits.Prestaciones.DTOs;
using Portable.FunctionalUnits.Prestaciones.Queries;
using Portable.FunctionalUnits.Prestaciones.Responses;
using Shared.Portable.Pagination;
using Test.IntegrationTests.Base;

namespace Test.IntegrationTests.Prestaciones
{
    public class NomencladorTestService : BaseTestService, IDisposable
    {
        public readonly string URL_GET_BY_ID = "/Prestaciones/GetNomencladorById";
        public readonly string URL_GET_PAGED = "/Prestaciones/GetNomencladorPaged";
        private const string URL_CREATE = "/Prestaciones/CreateNomenclador";
        private const string URL_UPDATE = "/Prestaciones/UpdateNomenclador";
        private const string URL_DELETE = "/Prestaciones/DeleteNomenclador";

        public NomencladorTestService(WebApplicationFactory<Program> factory) : base(factory)
        {
            cleanUp = false;
        }

        #region Actions

        #region Commands
        public async Task<CreateNomencladorResponse> Create(NomencladorDTO Nomenclador)
        {
            var command = new CreateNomencladorCommand
            {
                Nomenclador = Nomenclador
            };

            var response = await SendRequestAndDeserializeResponse<CreateNomencladorCommand, CreateNomencladorResponse>(URL_CREATE, command);

            // Se agrega a la lista cleanUpList para borrar la entidad una vez terminado el test.
            if (cleanUp && response.Success) cleanUpList.Add(response.Nomenclador!.Id);
            
            return response;
        }

        public async Task<UpdateNomencladorResponse> Update(NomencladorDTO Nomenclador)
        {
            var command = new UpdateNomencladorCommand
            {
                Nomenclador = Nomenclador
            };

            return await SendRequestAndDeserializeResponse<UpdateNomencladorCommand, UpdateNomencladorResponse>(URL_UPDATE, command);
        }

        public async Task<DeleteNomencladorResponse> Delete(int id)
        {
            var command = new DeleteNomencladorCommand
            {
                Id = id
            };

            return await SendRequestAndDeserializeResponse<DeleteNomencladorCommand, DeleteNomencladorResponse>(URL_DELETE, command);
        }
        #endregion

        #region Queries
        public async Task<GetNomencladorByIdResponse> GetById(int id)
        {
            // Arrange
            var query = new GetNomencladorByIdQuery { Id = id };

            // Act
            var response = await SendRequestAndDeserializeResponse<GetNomencladorByIdQuery, GetNomencladorByIdResponse>(URL_GET_BY_ID, query);

            return response;
        }

        public async Task<GetNomencladorPagedResponse> GetPaged(int pageNumber = 1, PaginationPageSize pageSize = PaginationPageSize.Default)
        {
            // Arrange
            var paginationParams = new PaginationParams()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var query = new GetNomencladorPagedQuery(paginationParams);

            // Act
            var response = await SendRequestAndDeserializeResponse<GetNomencladorPagedQuery, GetNomencladorPagedResponse>(URL_GET_PAGED, query);

            return response;
        }
        #endregion

        #endregion

        #region Getters
        public NomencladorDTO GetNomenclador(string? codigoNacional = null, string? descripcion = null, decimal? importe = null)
        {
            codigoNacional ??= RandomGenerator.GenerateRandomString(6);
            descripcion ??= RandomGenerator.GenerateRandomString(50);
            importe ??= 120000;

            var Nomenclador = new NomencladorDTO() 
            {
               CodigoNacional  = codigoNacional,
               Descripcion  = descripcion,
               Importe  = importe.Value,
            };

            return Nomenclador;
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

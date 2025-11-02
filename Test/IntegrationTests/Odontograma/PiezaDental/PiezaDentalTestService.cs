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
    public class PiezaDentalTestService : BaseTestService, IDisposable
    {
        public readonly string URL_GET_BY_ID = "/Odontogramas/GetPiezaDentalById";
        public readonly string URL_GET_PAGED = "/Odontogramas/GetPiezaDentalPaged";
        private const string URL_CREATE = "/Odontogramas/CreatePiezaDental";
        private const string URL_UPDATE = "/Odontogramas/UpdatePiezaDental";
        private const string URL_DELETE = "/Odontogramas/DeletePiezaDental";

        public PiezaDentalTestService(WebApplicationFactory<Program> factory) : base(factory)
        {
            //cleanUp = false;
        }

        #region Actions

        #region Commands
        public async Task<CreatePiezaDentalResponse> Create(PiezaDentalDTO PiezaDental)
        {
            var command = new CreatePiezaDentalCommand
            {
                PiezaDental = PiezaDental
            };

            var response = await SendRequestAndDeserializeResponse<CreatePiezaDentalCommand, CreatePiezaDentalResponse>(URL_CREATE, command);

            // Se agrega a la lista cleanUpList para borrar la entidad una vez terminado el test.
            if (cleanUp && response.Success) cleanUpList.Add(response.PiezaDental!.Id);
            
            return response;
        }

        public async Task<UpdatePiezaDentalResponse> Update(PiezaDentalDTO PiezaDental)
        {
            var command = new UpdatePiezaDentalCommand
            {
                PiezaDental = PiezaDental
            };

            return await SendRequestAndDeserializeResponse<UpdatePiezaDentalCommand, UpdatePiezaDentalResponse>(URL_UPDATE, command);
        }

        public async Task<DeletePiezaDentalResponse> Delete(int id)
        {
            var command = new DeletePiezaDentalCommand
            {
                Id = id
            };

            return await SendRequestAndDeserializeResponse<DeletePiezaDentalCommand, DeletePiezaDentalResponse>(URL_DELETE, command);
        }
        #endregion

        #region Queries
        public async Task<GetPiezaDentalByIdResponse> GetById(int id)
        {
            // Arrange
            var query = new GetPiezaDentalByIdQuery { Id = id };

            // Act
            var response = await SendRequestAndDeserializeResponse<GetPiezaDentalByIdQuery, GetPiezaDentalByIdResponse>(URL_GET_BY_ID, query);

            return response;
        }

        public async Task<GetPiezaDentalPagedResponse> GetPaged(int pageNumber = 1, PaginationPageSize pageSize = PaginationPageSize.Default)
        {
            // Arrange
            var paginationParams = new PaginationParams()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var query = new GetPiezaDentalPagedQuery(paginationParams);

            // Act
            var response = await SendRequestAndDeserializeResponse<GetPiezaDentalPagedQuery, GetPiezaDentalPagedResponse>(URL_GET_PAGED, query);

            return response;
        }
        #endregion

        #endregion

        #region Getters
        public PiezaDentalDTO GetPiezaDental(byte numeroPieza = 11, TipoCuadrante cuadrante = TipoCuadrante.SuperiorDerecho, bool denticionPermanente = true)
        {
            var PiezaDental = new PiezaDentalDTO() 
            {
                NumeroPieza = numeroPieza,
                Cuadrante = cuadrante,
                DenticionPermanente = denticionPermanente
            };

            return PiezaDental;
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

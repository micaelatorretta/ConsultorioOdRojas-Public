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
    public class CaraDentalTestService : BaseTestService, IDisposable
    {
        public readonly string URL_GET_BY_ID = "/Odontogramas/GetCaraDentalById";
        public readonly string URL_GET_PAGED = "/Odontogramas/GetCaraDentalPaged";
        private const string URL_CREATE = "/Odontogramas/CreateCaraDental";
        private const string URL_UPDATE = "/Odontogramas/UpdateCaraDental";
        private const string URL_DELETE = "/Odontogramas/DeleteCaraDental";

        public CaraDentalTestService(WebApplicationFactory<Program> factory) : base(factory)
        {
            cleanUp = false;
        }

        #region Actions

        #region Commands
        public async Task<CreateCaraDentalResponse> Create(CaraDentalDTO CaraDental)
        {
            var command = new CreateCaraDentalCommand
            {
                CaraDental = CaraDental
            };

            var response = await SendRequestAndDeserializeResponse<CreateCaraDentalCommand, CreateCaraDentalResponse>(URL_CREATE, command);

            // Se agrega a la lista cleanUpList para borrar la entidad una vez terminado el test.
            if (cleanUp && response.Success) cleanUpList.Add(response.CaraDental!.Id);
            
            return response;
        }

        public async Task<UpdateCaraDentalResponse> Update(CaraDentalDTO CaraDental)
        {
            var command = new UpdateCaraDentalCommand
            {
                CaraDental = CaraDental
            };

            return await SendRequestAndDeserializeResponse<UpdateCaraDentalCommand, UpdateCaraDentalResponse>(URL_UPDATE, command);
        }

        public async Task<DeleteCaraDentalResponse> Delete(int id)
        {
            var command = new DeleteCaraDentalCommand
            {
                Id = id
            };

            return await SendRequestAndDeserializeResponse<DeleteCaraDentalCommand, DeleteCaraDentalResponse>(URL_DELETE, command);
        }
        #endregion

        #region Queries
        public async Task<GetCaraDentalByIdResponse> GetById(int id)
        {
            // Arrange
            var query = new GetCaraDentalByIdQuery { Id = id };

            // Act
            var response = await SendRequestAndDeserializeResponse<GetCaraDentalByIdQuery, GetCaraDentalByIdResponse>(URL_GET_BY_ID, query);

            return response;
        }

        public async Task<GetCaraDentalPagedResponse> GetPaged(int pageNumber = 1, PaginationPageSize pageSize = PaginationPageSize.Default)
        {
            // Arrange
            var paginationParams = new PaginationParams()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var query = new GetCaraDentalPagedQuery(paginationParams);

            // Act
            var response = await SendRequestAndDeserializeResponse<GetCaraDentalPagedQuery, GetCaraDentalPagedResponse>(URL_GET_PAGED, query);

            return response;
        }
        #endregion

        #endregion

        #region Getters
        public CaraDentalDTO GetCaraDental(TipoCara caraDentaria = TipoCara.Mesial)
        {
            var CaraDental = new CaraDentalDTO() 
            {
                CaraDentaria = caraDentaria,
            };

            return CaraDental;
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

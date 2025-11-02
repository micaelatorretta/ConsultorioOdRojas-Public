using Microsoft.AspNetCore.Mvc.Testing;
using Portable.FunctionalUnits.Turnos.Commands;
using Portable.FunctionalUnits.Turnos.DTOs;
using Portable.FunctionalUnits.Turnos.Queries;
using Portable.FunctionalUnits.Turnos.Responses;
using Shared.Portable.Pagination;
using Test.IntegrationTests.Base;

namespace Test.IntegrationTests.Turnos
{
    public class TurnoTestService : BaseTestService, IDisposable
    {
        public readonly string URL_GET_BY_ID = "/Turnos/GetTurnoById";
        public readonly string URL_GET_PAGED = "/Turnos/GetTurnoPaged";
        private const string URL_CREATE = "/Turnos/CreateTurno";
        private const string URL_UPDATE = "/Turnos/UpdateTurno";
        private const string URL_DELETE = "/Turnos/DeleteTurno";

        public TurnoTestService(WebApplicationFactory<Program> factory) : base(factory)
        {
        }

        #region Actions

        #region Commands
        public async Task<CreateTurnoResponse> Create(TurnoDTO Turno)
        {
            var command = new CreateTurnoCommand
            {
                Turno = Turno
            };

            var response = await SendRequestAndDeserializeResponse<CreateTurnoCommand, CreateTurnoResponse>(URL_CREATE, command);

            // Se agrega a la lista cleanUpList para borrar la entidad una vez terminado el test.
            if (cleanUp && response.Success) cleanUpList.Add(response.Turno!.Id);
            
            return response;
        }

        public async Task<UpdateTurnoResponse> Update(TurnoDTO Turno)
        {
            var command = new UpdateTurnoCommand
            {
                Turno = Turno
            };

            return await SendRequestAndDeserializeResponse<UpdateTurnoCommand, UpdateTurnoResponse>(URL_UPDATE, command);
        }

        public async Task<DeleteTurnoResponse> Delete(int id)
        {
            var command = new DeleteTurnoCommand
            {
                Id = id
            };

            return await SendRequestAndDeserializeResponse<DeleteTurnoCommand, DeleteTurnoResponse>(URL_DELETE, command);
        }
        #endregion

        #region Queries
        public async Task<GetTurnoByIdResponse> GetById(int id)
        {
            // Arrange
            var query = new GetTurnoByIdQuery { Id = id };

            // Act
            var response = await SendRequestAndDeserializeResponse<GetTurnoByIdQuery, GetTurnoByIdResponse>(URL_GET_BY_ID, query);

            return response;
        }

        public async Task<GetTurnoPagedResponse> GetPaged(int pageNumber = 1, PaginationPageSize pageSize = PaginationPageSize.Default)
        {
            // Arrange
            var paginationParams = new PaginationParams()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var query = new GetTurnoPagedQuery(paginationParams);

            // Act
            var response = await SendRequestAndDeserializeResponse<GetTurnoPagedQuery, GetTurnoPagedResponse>(URL_GET_PAGED, query);

            return response;
        }
        #endregion

        #endregion

        #region Getters
        public TurnoDTO GetTurno(string? descripcion = null, DateOnly? fecha = null)
        {
            var Turno = new TurnoDTO() 
            { 
                Descripcion = descripcion ?? "Turno de Prueba",
                Fecha = fecha ?? DateOnly.FromDateTime(DateTime.Now)
            };

            return Turno;
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

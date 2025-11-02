using Microsoft.AspNetCore.Mvc.Testing;
using Portable.Enums.Auth;
using Portable.FunctionalUnits.Pacientes.Commands;
using Portable.FunctionalUnits.Pacientes.DTOs;
using Portable.FunctionalUnits.Pacientes.Queries;
using Portable.FunctionalUnits.Pacientes.Responses;
using Shared.Portable.Filters;
using Shared.Portable.FiltersAndSort;
using Shared.Portable.Pagination;
using Test.IntegrationTests.Base;

namespace Test.IntegrationTests.Pacientes
{
    public class PacienteTestService : BaseTestService, IDisposable
    {
        public readonly string URL_GET_BY_ID = "/Pacientes/GetPacienteById";
        public readonly string URL_GET_PAGED = "/Pacientes/GetPacientePaged";
        private const string URL_CREATE = "/Pacientes/CreatePaciente";
        private const string URL_UPDATE = "/Pacientes/UpdatePaciente";
        private const string URL_DELETE = "/Pacientes/DeletePaciente";

        public PacienteTestService(WebApplicationFactory<Program> factory) : base(factory)
        {
        }

        #region Actions

        #region Commands
        public async Task<CreatePacienteResponse> Create(PacienteDTO Paciente)
        {
            var command = new CreatePacienteCommand
            {
                Paciente = Paciente
            };

            var response = await SendRequestAndDeserializeResponse<CreatePacienteCommand, CreatePacienteResponse>(URL_CREATE, command);

            // Se agrega a la lista cleanUpList para borrar la entidad una vez terminado el test.
            if (cleanUp && response.Success) cleanUpList.Add(response.Paciente!.Id);
            
            return response;
        }

        public async Task<UpdatePacienteResponse> Update(PacienteDTO Paciente)
        {
            var command = new UpdatePacienteCommand
            {
                Paciente = Paciente
            };

            return await SendRequestAndDeserializeResponse<UpdatePacienteCommand, UpdatePacienteResponse>(URL_UPDATE, command);
        }

        public async Task<DeletePacienteResponse> Delete(int id)
        {
            var command = new DeletePacienteCommand
            {
                Id = id
            };

            return await SendRequestAndDeserializeResponse<DeletePacienteCommand, DeletePacienteResponse>(URL_DELETE, command);
        }
        #endregion

        #region Queries
        public async Task<GetPacienteByIdResponse> GetById(int id)
        {
            // Arrange
            var query = new GetPacienteByIdQuery { Id = id };

            // Act
            var response = await SendRequestAndDeserializeResponse<GetPacienteByIdQuery, GetPacienteByIdResponse>(URL_GET_BY_ID, query);

            return response;
        }

        public async Task<GetPacientePagedResponse> GetPaged(int pageNumber = 1, 
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
            var query = new GetPacientePagedQuery(paginationParams);

            // Act
            var response = await SendRequestAndDeserializeResponse<GetPacientePagedQuery, GetPacientePagedResponse>(URL_GET_PAGED, query);

            return response;
        }
        #endregion

        #endregion

        #region Getters
        public PacienteDTO GetPaciente(string? nombre = null, 
                                     string? apellido = null, 
                                     string? dni = null)
        {
            var Paciente = new PacienteDTO()
            {
                Nombre = nombre ?? "Nombre de Prueba",
                Apellido = apellido ?? "Apellido de Prueba",
                DNI = dni ?? "35654789"
            };

            return Paciente;
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

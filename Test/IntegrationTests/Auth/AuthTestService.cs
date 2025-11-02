using Microsoft.AspNetCore.Mvc.Testing;
using Portable.FunctionalUnits.Auth.Commands;
using Portable.FunctionalUnits.Auth.DTOs;
using Portable.FunctionalUnits.Auth.Responses;
using Test.IntegrationTests.Base;

namespace Test.IntegrationTests.Auth
{
    public class AuthTestService : BaseTestService, IDisposable
    {
        public readonly string URL_LOGIN = "/Auth/Login";
     

        public AuthTestService(WebApplicationFactory<Program> factory) : base(factory)
        {
        }

        #region Actions

        #region Commands
        public async Task<LoginResponse> Login(LoginCredentialsDTO loginCredentials)
        {
            var command = new LoginCommand
            {
                LoginCredentials = loginCredentials
            };

            var response = await SendRequestAndDeserializeResponse<LoginCommand, LoginResponse>(URL_LOGIN, command);
            
            return response;
        }

     
        #endregion

        #endregion

        #region Getters
        public LoginCredentialsDTO GetLoginCredentials(string username, string password)
        {
            var loginCredentials = new LoginCredentialsDTO()
            {
                Username = username,
                Password = password
            };

            return loginCredentials;
        }



        #endregion


        public void Dispose()
        {
            if (cleanUp)
            {
                //cleanUpList.ForEach(i => Delete(i).Wait());
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc.Testing;

namespace Test.IntegrationTests.Base
{
    public abstract class BaseTest : IClassFixture<WebApplicationFactory<Program>>
    {


    }
}

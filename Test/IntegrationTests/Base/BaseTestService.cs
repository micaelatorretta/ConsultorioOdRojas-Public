using Newtonsoft.Json;
using Shared.Portable.Base.Responses;
using Shared.Portable.Base;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Serialization;
using Test.Utils;

namespace Test.IntegrationTests.Base
{
    public class BaseTestService 
    {
        public RandomGenerator RandomGenerator { get; } = new RandomGenerator();

        /// <summary>
        /// Indica si debe eliminar las entidades que se crean cuando se ejecutan los tests.
        /// </summary>
        protected bool cleanUp = true;
        /// <summary>
        /// Lista donde se van agregando las entidades creadas para luego de ejecutado el test se eliminen.
        /// </summary>
        protected virtual List<int> cleanUpList { get; set; } = new();

        protected readonly WebApplicationFactory<Program> _factory;
        protected readonly HttpClient _client;
        protected readonly JsonSerializerSettings _jsonSettings;

        protected BaseTestService(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
            // Configuración personalizada para JSON
            _jsonSettings = new JsonSerializerSettings
            {
                // Cambia la convención de nombres (de PascalCase a camelCase)
                ContractResolver = new CamelCasePropertyNamesContractResolver(),

                // Ignora valores nulos al serializar
                NullValueHandling = NullValueHandling.Ignore,
                
                MaxDepth = null,
                PreserveReferencesHandling = PreserveReferencesHandling.All,

                // Formateo de la salida JSON para que sea más legible (indentar)
                Formatting = Formatting.Indented,

                // Manejo de ciclos de referencia (importante si trabajas con EF y relaciones circulares)
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize
            };
        }

        // Método genérico para deserializar el contenido de la respuesta JSON
        protected T DeserializeResponse<T>(string responseContent)
        {
            return JsonConvert.DeserializeObject<T>(responseContent, _jsonSettings);
        }

        // Método helper para enviar una consulta y deserializar directamente
        protected async Task<TResponse> SendRequestAndDeserializeResponse<TRequest, TResponse>(string url, TRequest requestData)
            where TRequest : BaseRequest
            where TResponse : BaseResponse
        {
            string? responseContent;
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(requestData, _jsonSettings), Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(url, content);
                // response.EnsureSuccessStatusCode(); // Lanza una excepción si la respuesta no es exitosa
                responseContent = await response.Content.ReadAsStringAsync();
                var deserializedResponse = DeserializeResponse<TResponse>(responseContent);

                if (deserializedResponse is null) throw new Exception(responseContent);

                return deserializedResponse;
            }
            catch (Exception ex) 
            {
                throw;
            }
        }
    }
}

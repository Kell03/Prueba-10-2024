using Practice_wasm.Interface;
using Practice_wasm.Utils;
using Radzen;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Net.Http.Headers;


namespace Practice_wasm.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;
        private readonly string _method;
        private readonly string _path;
       private readonly AuthenticationStateProvider _authenticationStateProvider;

        public GenericService(HttpClient httpClient, string url, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _url = url;
            _authenticationStateProvider = authenticationStateProvider;

        }

        public async Task<Result<PaginatedList<T>>> GetAll(string? additionalPath = "") // Método asincrónico
        {
            try
            {
              
                var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                var tokenClaim = authState.User.FindFirst("Token");
               var token = tokenClaim?.Value; // Accede al token

                var request = new HttpRequestMessage(HttpMethod.Get, $"{_url}/{additionalPath}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token); 

                // Enviar la solicitud con el encabezado de autorización
                var response = await _httpClient.SendAsync(request);


                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Result<PaginatedList<T>>>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Result<T>> GetById(int id, string? additionalPath = "")
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var token = authState.User.FindFirst("Token")?.Value;

            var url = additionalPath != "" ? $"{_url}/{additionalPath}/{id}" : $"{_url}/{id}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Result<T>>();
        }

        public async Task<Result<T>> Add(T entity, string? additionalPath = "")
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var token = authState.User.FindFirst("Token")?.Value;

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_url}/{additionalPath}")
            {
                Content = JsonContent.Create(entity) // Enviar la entidad como JSON
            };

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Result<T>>();
        }
        public async Task<Result<T>> Update(int id, T entity, string? additionalPath = "")
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var token = authState.User.FindFirst("Token")?.Value;

            var request = new HttpRequestMessage(HttpMethod.Put, $"{_url}/{additionalPath}/{id}")
            {
                Content = JsonContent.Create(entity) // Convertir la entidad a JSON
            };

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Result<T>>();
        }

        public async Task<Result<bool>> Delete(int id, string additionalPath = "")
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var token = authState.User.FindFirst("Token")?.Value;

            var request = new HttpRequestMessage(HttpMethod.Delete, $"{_url}/{additionalPath}/{id}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Result<bool>>();
        }
    }
}
    

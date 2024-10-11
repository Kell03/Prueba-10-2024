using Practice_wasm.Interface;
using Practice_wasm.Models;
using Practice_wasm.Utils;
using Radzen;
using System.Net.Http.Json;

namespace Practice_wasm.Services
{
    public class LoginService : IloginService
    {

        private readonly HttpClient _httpClient;
        public LoginService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginResponse<UsuarioDto>> login(UsuarioDto usuario)
        {

            try
            {
                var response = await _httpClient.PostAsJsonAsync("login", usuario);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<LoginResponse<UsuarioDto>>();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}

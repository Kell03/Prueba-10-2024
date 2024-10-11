using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Practice_wasm.Models;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Practice_wasm.Extensiones
{
    public class AutenticacionExtension : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorageService;
        private ClaimsPrincipal _sinInformacion = new ClaimsPrincipal();


        public AutenticacionExtension(ISessionStorageService sessionStorage)
        {
            _sessionStorageService = sessionStorage;

        }

        public async Task ActualizarEstadoAutenticacion(UsuarioDto? sessionUsuario, string? token = "")
        {
            ClaimsPrincipal claimsPrincipal;
            if (token != "" && token != null) {
                sessionUsuario.Token = token ;
            }

            if (sessionUsuario != null)
            {
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name,sessionUsuario.NombreUsuario),
                    new Claim(ClaimTypes.Email,sessionUsuario.Email),
                    new Claim("Token", sessionUsuario.Token), // Ejemplo para agregar el rol
                    new Claim("Id", sessionUsuario.Id.ToString()), // Ejemplo para agregar el rol


                }, "JwtAuth"));

                await _sessionStorageService.GuardarStorage("SesionUsuario", sessionUsuario);
            }
            else
            {
                claimsPrincipal = _sinInformacion;
                await _sessionStorageService.RemoveItemAsync("SesionUsuario");

            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));

        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var sessionUsuario = await _sessionStorageService.ObtenerStorage<UsuarioDto>("SesionUsuario");
            if (sessionUsuario == null)
            {
                return await Task.FromResult(new AuthenticationState(_sinInformacion));
            }
            else
            {
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
        {
            new Claim(ClaimTypes.Name, sessionUsuario.NombreUsuario),
            new Claim(ClaimTypes.Email, sessionUsuario.Email),
            new Claim("Token", sessionUsuario.Token), // Asumiendo que el token está en UsuarioDto
            new Claim("Id", sessionUsuario.Id.ToString()) // Asumiendo que Id es un campo de UsuarioDto
        }, "JwtAuth"));
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));

            }

        }
    }
}

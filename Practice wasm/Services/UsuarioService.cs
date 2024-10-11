using Microsoft.AspNetCore.Components.Authorization;
using Practice_wasm.Interface;
using Practice_wasm.Models;
using Practice_wasm.Utils;

namespace Practice_wasm.Services
{
    public class UsuarioService: IUsuarioService
    {
        private readonly GenericService<UsuarioDto> _genericService;

        public UsuarioService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
        {
            _genericService = new GenericService<UsuarioDto>(httpClient, "usuario", authenticationStateProvider);
        }

        public async Task<Result<UsuarioDto>> Add(UsuarioDto usuario)
        {
            return await _genericService.Add(usuario);
        }

        public async Task<Result<bool>> Delete(int id)
        {
            return await _genericService.Delete(id, "delete");
        }

        public async Task<Result<PaginatedList<UsuarioDto>>> GetAll()
        {
            try
            {
                string path = "0/0"; // Asumiendo que este path maneja paginación (index/take)

                var lista = await _genericService.GetAll(path);
                return lista;
            }
            catch (Exception ex)
            {
                return Result<PaginatedList<UsuarioDto>>.Failure(ex.Message);
            }
        }

        public async Task<Result<UsuarioDto>> GetById(int id)
        {
            return await _genericService.GetById(id);
        }

        public async Task<Result<UsuarioDto>> Update(int id, UsuarioDto usuario)
        {
            return await _genericService.Update(id, usuario, "edit");
        }

    }
}

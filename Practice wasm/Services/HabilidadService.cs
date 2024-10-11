using Microsoft.AspNetCore.Components.Authorization;
using Practice_wasm.Interface;
using Practice_wasm.Models;
using Practice_wasm.Utils;

namespace Practice_wasm.Services
{
    public class HabilidadService : IHabilidadService
    {
        private readonly GenericService<HabilidadDto> _genericService;

        public HabilidadService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
        {
            _genericService = new GenericService<HabilidadDto>(httpClient, "habilidad", authenticationStateProvider);
        }

        public async Task<Result<HabilidadDto>> Add(HabilidadDto habilidad)
        {
            return await _genericService.Add(habilidad);
        }

        public async Task<Result<bool>> Delete(int id)
        {
            return await _genericService.Delete(id, "delete");
        }

        public async Task<Result<PaginatedList<HabilidadDto>>> GetAll()
        {
            try
            {
                string path = "0/0";

                var lista = await _genericService.GetAll(path);
                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Result<HabilidadDto>> GetById(int id)
        {
            return await _genericService.GetById(id);
        }

        public async Task<Result<HabilidadDto>> Update(int id, HabilidadDto habilidad)
        {
            return await _genericService.Update(id, habilidad, "edit");
        }
    }
}

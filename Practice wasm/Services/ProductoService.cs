using Microsoft.AspNetCore.Components.Authorization;
using Practice_wasm.Interface;
using Practice_wasm.Models;
using Practice_wasm.Utils;

namespace Practice_wasm.Services
{
    public class ProductoService: IProductoService
    {

        private readonly GenericService<ProductoDto> _genericService;


        public ProductoService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
        {
            _genericService = new GenericService<ProductoDto>(httpClient, "producto", authenticationStateProvider);

        }

        public async Task<Result<ProductoDto>> Add(ProductoDto producto)
        {

            return await _genericService.Add(producto);
        }

        public async Task<Result<bool>> Delete(int id)
        {
            return await _genericService.Delete(id, "delete");
        }

        public async Task<Result<PaginatedList<ProductoDto>>> GetAll()
        {
            try
            {
                string path = "0/0"; // Asumiendo que este path maneja paginación (index/take)
                var lista = await _genericService.GetAll(path);
                return lista;
            }
            catch (Exception ex)
            {
                return Result<PaginatedList<ProductoDto>>.Failure(ex.Message);
            }
        }

        public async Task<Result<ProductoDto>> GetById(int id)
        {
            return await _genericService.GetById(id);
        }

        public async Task<Result<ProductoDto>> Update(int id, ProductoDto producto)
        {
            return await _genericService.Update(id, producto, "edit");
        }
    }
}

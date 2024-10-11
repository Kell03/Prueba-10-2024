using Microsoft.AspNetCore.Components.Authorization;
using Practice_wasm.Interface;
using Practice_wasm.Models;
using Practice_wasm.Utils;

namespace Practice_wasm.Services
{
    public class IventarioService : IInventarioService
    {
        private readonly GenericService<InventarioDto> _genericService;



        public IventarioService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
        {
            _genericService = new GenericService<InventarioDto>(httpClient, "inventario", authenticationStateProvider);

        }

        public async Task<Result<InventarioDto>> Add(ProductoDto productodto)
        {
            try
            {
                InventarioDto inventario = new InventarioDto();
                inventario.Cantidad = productodto.CantidadDisponible;
                inventario.ProductoId = productodto.Id;
                inventario.TipoMovimiento = "Entrada";
                inventario.FechaMovimiento = DateTime.Now; // Guarda la fecha actual
                return await _genericService.Add(inventario);
            }
            catch (Exception ex)
            {
                return new Result<InventarioDto>();
            }
        }

        public async Task<Result<bool>> Delete(int id)
        {
            //return await _genericService.Delete(id, "delete");
            // Verificar si el producto ya está en el inventario
            var inventarioExistente = await _genericService.Delete(id, "deleteproducts");
            if (inventarioExistente.IsSuccess)
            {
                return Result<bool>.Success(true, "Productos Eliminados del inventario"); // Sin cambios

            }
            else
            {
                return Result<bool>.Failure(inventarioExistente.message); // Sin cambios

            }

        }

        public async Task<Result<PaginatedList<InventarioDto>>> GetAll()
        {
            try
            {
                string path = "0/0"; // Asumiendo que este path maneja paginación (index/take)

                var lista = await _genericService.GetAll(path);
                return lista;
            }
            catch (Exception ex)
            {
                return Result<PaginatedList<InventarioDto>>.Failure(ex.Message);
            }
        }

        public async Task<Result<InventarioDto>> GetById(int id)
        {
            return await _genericService.GetById(id);
        }

        public async Task<Result<InventarioDto>> Update(ProductoDto productodto)
        {

            // Verificar si el producto ya está en el inventario
            var inventarioExistente = await _genericService.GetById(productodto.Id, "lastproduct");

            if (inventarioExistente == null)
            {
                return await Add(productodto); // Si no existe, llamamos a Add
            }

            // Comparar la cantidad existente con la nueva cantidad
            int cantidadExistente = inventarioExistente.Value.Cantidad;
            int cantidadNueva = productodto.CantidadDisponible;

            if (cantidadNueva > cantidadExistente)
            {
                int cantidadADisponible = cantidadNueva - cantidadExistente;
                InventarioDto nuevoInventario = new InventarioDto
                {
                    Cantidad = cantidadADisponible,
                    ProductoId = productodto.Id,
                    TipoMovimiento = "Entrada",
                    FechaMovimiento = DateTime.Now
                };

                return await _genericService.Add(nuevoInventario);
            }
            else if (cantidadNueva < cantidadExistente)
            {
                int cantidadASalir = cantidadExistente - cantidadNueva;
                InventarioDto nuevoInventario = new InventarioDto
                {
                    Cantidad = cantidadASalir,
                    ProductoId = productodto.Id,
                    TipoMovimiento = "Salida",
                    FechaMovimiento = DateTime.Now
                };

                return await _genericService.Add(nuevoInventario);
            }


            return Result<InventarioDto>.Success(inventarioExistente.Value, "Sin cambios"); // Sin cambios
        }

    }
}

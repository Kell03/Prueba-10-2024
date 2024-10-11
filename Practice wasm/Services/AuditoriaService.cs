using Microsoft.AspNetCore.Components.Authorization;
using Practice_wasm.Interface;
using Practice_wasm.Models;
using Practice_wasm.Utils;
using System;

namespace Practice_wasm.Services
{
    public class AuditoriaService:IAuditoriaService
    {
        private readonly GenericService<AuditoriaDto> _genericService;

        public AuditoriaService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
        {
            _genericService = new GenericService<AuditoriaDto>(httpClient, "auditoria", authenticationStateProvider);
        }

        public async Task<Result<AuditoriaDto>> Add(ProductoDto productoDto, string accion, int? cantidad = 0)
        {
            try
            {
                AuditoriaDto auditoriaDto = new AuditoriaDto();
                auditoriaDto.ProductoId = productoDto.Id;
                auditoriaDto.Cantidad = (cantidad == 0)? productoDto.CantidadDisponible: cantidad;
                auditoriaDto.Accion = accion;
                auditoriaDto.UsuarioId = 2;
                auditoriaDto.Motivo = "";
                return await _genericService.Add(auditoriaDto);
            }
            catch (Exception ex)
            {
                return Result<AuditoriaDto>.Failure(ex.Message);
            }
        }

        public async Task<Result<bool>> Delete(int id)
        {
            var auditoriaExistente = await _genericService.Delete(id, "delete");
            if (auditoriaExistente.IsSuccess)
            {
                return Result<bool>.Success(true, "Registro de auditoría eliminado.");
            }
            else
            {
                return Result<bool>.Failure(auditoriaExistente.message);
            }
        }

        public async Task<Result<PaginatedList<AuditoriaDto>>> GetAll()
        {
            try
            {
                string path = "0/0"; // Suponiendo que este path maneja paginación (index/take)
                return await _genericService.GetAll(path);
            }
            catch (Exception ex)
            {
                return Result<PaginatedList<AuditoriaDto>>.Failure(ex.Message);
            }
        }

        public async Task<Result<AuditoriaDto>> GetById(int id)
        {
            return await _genericService.GetById(id);

            //jsjksjkdsjdjdkkjdskjds
        }


        public async Task<Result<AuditoriaDto>> Update(ProductoDto productodto)
        {
            // Verificar si el producto ya tiene registros en la auditoría
            var auditoriaExistente = await _genericService.GetById(productodto.Id, "lastproduct");

            if (auditoriaExistente == null)
            {
                return await Add(productodto, "Compra"); // Si no existe, llamamos a Add
            }

            // Comparar la cantidad existente con la nueva cantidad
            int cantidadExistente = auditoriaExistente.Value.Cantidad ?? 0;
            int cantidadNueva = productodto.CantidadDisponible;

            if (cantidadNueva > cantidadExistente)
            {
                int cantidadADisponible = cantidadNueva - cantidadExistente;
                AuditoriaDto nuevaAuditoria = new AuditoriaDto
                {
                    Cantidad = cantidadADisponible,
                    ProductoId = productodto.Id,
                    Accion = "Compra",
                    UsuarioId = 2,
                    Motivo = ""
                };

                return await _genericService.Add(nuevaAuditoria);
            }
            else if (cantidadNueva < cantidadExistente)
            {
                int cantidadASalir = cantidadExistente - cantidadNueva;
                AuditoriaDto nuevaAuditoria = new AuditoriaDto
                {
                    Cantidad = cantidadASalir,
                    ProductoId = productodto.Id,
                    Accion = "Salida",
                    UsuarioId = 2,
                    Motivo = ""
                };

                return await _genericService.Add(nuevaAuditoria);
            }

            return Result<AuditoriaDto>.Success(auditoriaExistente.Value, "Sin cambios");
        }

       
    }
}

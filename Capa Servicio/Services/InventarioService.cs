using AutoMapper;
using Capa_Acceso_Datos.Repository;
using Capa_Servicio.Interface;
using CapaDominio.Modeldto;
using CapaDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Acceso_Datos.Utils;
using System.Linq.Expressions;

namespace Capa_Servicio.Services
{
    public class InventarioService: IInventario
    {

        private readonly IMapper _mapper;
        private readonly IRepository<Inventario> _repository;

        public InventarioService(IMapper mapper, IRepository<Inventario> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Result<Inventariodto>> Add(Inventario inventario)
        {
            try
            {
                var resultado = await _repository.Add(inventario);

                if (!resultado.IsSuccess)
                {
                    return Result<Inventariodto>.Failure(resultado.message);
                }
                else
                {
                    var inventariodto = _mapper.Map<Inventariodto>(resultado.Value);
                    return Result<Inventariodto>.Success(inventariodto, resultado.message);
                }
            }
            catch (Exception ex)
            {
                return Result<Inventariodto>.Failure(ex.Message);
            }
        }

        public async Task<Result<bool>> Delete(int id)
        {
            try
            {
                var resultado = await _repository.Delete(id);

                if (!resultado.IsSuccess)
                {
                    return Result<bool>.Failure(resultado.message);
                }
                else
                {
                    return Result<bool>.Success(true, resultado.message);
                }
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }

        public async Task<Result<PaginatedListdto<Inventariodto>>> GetAll(int index, int take)
        {
            try
            {
                // Crear el objeto GetRequest con un Include
                var request = new GetRequest<Inventario>
                {
                    Includes = new List<Expression<Func<Inventario, object>>>
            {
                p => p.Producto // Incluir la relación con Producto
            }
                };


                var resultado = await _repository.GetAll(request, index, take);

                if (!resultado.IsSuccess)
                {
                    return Result<PaginatedListdto<Inventariodto>>.Failure(resultado.message);
                }
                else
                {
                    var listadto = _mapper.Map<PaginatedListdto<Inventariodto>>(resultado.Value);
                    return Result<PaginatedListdto<Inventariodto>>.Success(listadto, resultado.message);
                }
            }
            catch (Exception ex)
            {
                return Result<PaginatedListdto<Inventariodto>>.Failure(ex.Message);
            }
        }




        public async Task<Result<Inventariodto>> getExitsProduct(int id)
        {

            try
            {
                // Crear el objeto GetRequest con un filtro para obtener el último registro
                var request = new GetRequest<Inventario>
                {
                    Filter = inventario => inventario.ProductoId == id, // Filtrar por ProductId
                    OrderBy = query => query.OrderByDescending(inv => inv.Id), // Ordenar por Id en orden descendente
                    Includes = new List<Expression<Func<Inventario, object>>>
            {
                p => p.Producto // Incluir la relación con Producto
            }
                };

                // Obtener todos los registros del inventario que coinciden con el filtro
                var resultado = await _repository.GetAll(request);

                // Verificar si se encontró algún resultado
                if (resultado.Value?.Data == null || !resultado.Value.Data.Any())
                {
                    return Result<Inventariodto>.Failure("El producto no existe en el inventario.");
                }

                // Obtener el primer elemento de la lista que ya está ordenada
                var ultimoRegistro = resultado.Value.Data.FirstOrDefault();

                return Result<Inventariodto>.Success(_mapper.Map<Inventariodto>(ultimoRegistro), "Último registro obtenido exitosamente.");
            }
            catch (Exception ex)
            {
                return Result<Inventariodto>.Failure(ex.Message);
            }

        }


        public async Task<Result<bool>> DeleteAllById(int id)
        {

            try
            {
                // Crear el objeto GetRequest con un filtro para obtener el último registro
                var request = new GetRequest<Inventario>
                {
                    Filter = inventario => inventario.ProductoId == id, // Filtrar por ProductId
                };

                // Obtener todos los registros del inventario que coinciden con el filtro
                var resultado = await _repository.DeleteAllById(request);

                if (resultado.IsSuccess)
                {
                    return Result<bool>.Success(true, "Productos Eliminados del Inventario.");
                }
                else
                {
                    return Result<bool>.Failure(resultado.message);

                }
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }

        }

        public async Task<Result<Inventariodto>> GetById(int id)
        {
            try
            {
                var resultado = await _repository.GetById(id);

                if (!resultado.IsSuccess)
                {
                    return Result<Inventariodto>.Failure(resultado.message);
                }
                else
                {
                    var inventariodto = _mapper.Map<Inventariodto>(resultado.Value);
                    return Result<Inventariodto>.Success(inventariodto, resultado.message);
                }
            }
            catch (Exception ex)
            {
                return Result<Inventariodto>.Failure(ex.Message);
            }
        }

        public async Task<Result<Inventariodto>> Update(Inventario inventario)
        {
            try
            {
                var resultado = await _repository.Update(inventario);

                if (!resultado.IsSuccess)
                {
                    return Result<Inventariodto>.Failure(resultado.message);
                }
                else
                {
                    var inventariodto = _mapper.Map<Inventariodto>(resultado.Value);
                    return Result<Inventariodto>.Success(inventariodto, resultado.message);
                }
            }
            catch (Exception ex)
            {
                return Result<Inventariodto>.Failure(ex.Message);
            }
        }
    }
}


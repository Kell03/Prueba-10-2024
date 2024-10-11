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
    public class AuditoriaService : IAuditoria
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Auditoria> _repository;

        public AuditoriaService(IMapper mapper, IRepository<Auditoria> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Result<Auditoriadto>> Add(Auditoria auditoria)
        {
            try
            {
                var resultado = await _repository.Add(auditoria);

                if (!resultado.IsSuccess)
                {
                    return Result<Auditoriadto>.Failure(resultado.message);
                }
                else
                {
                    var auditoriadto = _mapper.Map<Auditoriadto>(resultado.Value);
                    return Result<Auditoriadto>.Success(auditoriadto, resultado.message);
                }
            }
            catch (Exception ex)
            {
                return Result<Auditoriadto>.Failure(ex.Message);
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

        public async Task<Result<PaginatedListdto<Auditoriadto>>> GetAll(int index, int take)
        {
            try
            {
                var request = new GetRequest<Auditoria>
                {
                    Includes = new List<Expression<Func<Auditoria, object>>>
            {
                p => p.Producto,
                s => s.Usuario// Incluir la relación con Producto
            }
                };
                var resultado = await _repository.GetAll(request, index, take);

                if (!resultado.IsSuccess)
                {
                    return Result<PaginatedListdto<Auditoriadto>>.Failure(resultado.message);
                }
                else
                {
                    var listadto = _mapper.Map<PaginatedListdto<Auditoriadto>>(resultado.Value);
                    return Result<PaginatedListdto<Auditoriadto>>.Success(listadto, resultado.message);
                }
            }
            catch (Exception ex)
            {
                return Result<PaginatedListdto<Auditoriadto>>.Failure(ex.Message);
            }
        }


        public async Task<Result<Auditoriadto>> getExitsProduct(int id)
        {

            try
            {
                // Crear el objeto GetRequest con un filtro para obtener el último registro
                var request = new GetRequest<Auditoria>
                {
                    Filter = inventario => inventario.ProductoId == id, // Filtrar por ProductId
                    OrderBy = query => query.OrderByDescending(inv => inv.Id), // Ordenar por Id en orden descendente
                    Includes = new List<Expression<Func<Auditoria, object>>>
            {
                p => p.Producto, // Incluir la relación con Producto
                s => s.Usuario// Incluir la relación con Producto

            }
                };

                // Obtener todos los registros del inventario que coinciden con el filtro
                var resultado = await _repository.GetAll(request);

                // Verificar si se encontró algún resultado
                if (resultado.Value?.Data == null || !resultado.Value.Data.Any())
                {
                    return Result<Auditoriadto>.Failure("El producto no existe en la auditoria.");
                }

                // Obtener el primer elemento de la lista que ya está ordenada
                var ultimoRegistro = resultado.Value.Data.FirstOrDefault();

                return Result<Auditoriadto>.Success(_mapper.Map<Auditoriadto>(ultimoRegistro), "Último registro obtenido exitosamente.");
            }
            catch (Exception ex)
            {
                return Result<Auditoriadto>.Failure(ex.Message);
            }

        }


        public async Task<Result<Auditoriadto>> GetById(int id)
        {
            try
            {
                var resultado = await _repository.GetById(id);

                if (!resultado.IsSuccess)
                {
                    return Result<Auditoriadto>.Failure(resultado.message);
                }
                else
                {
                    var auditoriadto = _mapper.Map<Auditoriadto>(resultado.Value);
                    return Result<Auditoriadto>.Success(auditoriadto, resultado.message);
                }
            }
            catch (Exception ex)
            {
                return Result<Auditoriadto>.Failure(ex.Message);
            }
        }

        public async Task<Result<Auditoriadto>> Update(Auditoria auditoria)
        {
            try
            {
                var resultado = await _repository.Update(auditoria);

                if (!resultado.IsSuccess)
                {
                    return Result<Auditoriadto>.Failure(resultado.message);
                }
                else
                {
                    var auditoriadto = _mapper.Map<Auditoriadto>(resultado.Value);
                    return Result<Auditoriadto>.Success(auditoriadto, resultado.message);
                }
            }
            catch (Exception ex)
            {
                return Result<Auditoriadto>.Failure(ex.Message);
            }
        }
    }
    
}

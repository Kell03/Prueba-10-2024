using Capa_Acceso_Datos.Utils;
using CapaDominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Acceso_Datos.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly ApipracticeContext _context;

        public GenericRepository(ApipracticeContext context)
        {
            _context = context;
        }

        public async Task<Result<T>> Add(T entity)
        {

            try
            {
                var addedEntity = (await _context.AddAsync(entity)).Entity;
                _context.SaveChanges();

                return Result<T>.Success(addedEntity, "Registro realizado exitosamente.");

            }
            catch (Exception ex) {

                return Result<T>.Failure(ex.Message); // Mensaje de error

            }
        }

        public async Task<Result<bool>> Delete(int entityId)
        {
            try
            {
                var entity = _context.Find<T>(entityId);
                if (entity != null) _context.Remove(entity);
                _context.SaveChanges();

                return Result<bool>.Success(true, "Registro eliminado exitosamente.");

            }
            catch (Exception ex)
            {

                return Result<bool>.Failure(ex.Message); // Mensaje de error

            }
        }

        public async Task<Result<PaginatedList<T>>> GetAll(GetRequest<T>? request = null, int? pageIndex = 0, int? pageSize = 0)
        {

            try
            {
                IQueryable<T> query = _context.Set<T>();

                if (request != null)
                {
                    // Aplicar filtros
                    if (request.Filter != null)
                    {
                        query = query.Where(request.Filter);
                    }

                    // Aplicar ordenamientos
                    if (request.OrderBy != null)
                    {
                        query = request.OrderBy(query);
                    }

                    // Aplicar includes solo si hay elementos en la lista
                    if (request.Includes != null && request.Includes.Count > 0) // Verificar si hay Includes
                    {
                        foreach (var include in request.Includes)
                        {
                            query = query.Include(include); // Incluir las propiedades especificadas
                        }
                    }
                }
                // Obtener el recuento total de elementos
                var count = await query.CountAsync();
                var totalPages = pageSize.HasValue && pageSize.Value > 0
                                 ? (int)Math.Ceiling(count / (double)pageSize)
                                 : 1; // Si no hay pageSize, consideramos que hay una sola página

                if (pageIndex.Value > 0 && pageSize.Value >0)
                {
                    // Aplicar Skip y Take para la paginación
                    var items = await query.Skip((pageIndex.Value - 1) * pageSize.Value)
                                            .Take(pageSize.Value)
                                            .ToListAsync();
                    var total = items.Count;

                    var paginatedList = new PaginatedList<T>(items, pageSize.Value, pageSize.Value, total);
                    

                    return Result<PaginatedList<T>>.Success(paginatedList, "Consulta realizada exitosamente.");
                }
                else
                {
                    // Obtener todos los registros si no hay paginación
                    var items = await query.ToListAsync();
                    var total = items.Count;

                    var paginatedList = new PaginatedList<T>(items, pageSize.Value, pageSize.Value, total);


                    return Result<PaginatedList<T>>.Success(paginatedList, "Consulta realizada exitosamente.");
                }
            }
            catch (Exception ex)
            {
                return Result<PaginatedList<T>>.Failure(ex.Message); // Mensaje de error
            }
        }

        public  async Task<Result<T>>? GetById(int entityId)
        {
            try
            {
                var entity = await _context.FindAsync<T>(entityId);
                await _context.SaveChangesAsync();

                return Result<T>.Success(entity, "Registro obtenido exitosamente.");

            }
            catch (Exception ex)
            {

                return Result<T>.Failure(ex.Message); // Mensaje de error

            }

        }

        public async Task<Result<T>> Update(T entity)
        {
            try
            {
                var updatedEntity = _context.Update(entity).Entity;
                await _context.SaveChangesAsync();

                return Result<T>.Success(updatedEntity, "Registro actualizado exitosamente.");

            }
            catch (Exception ex)
            {

                return Result<T>.Failure(ex.Message); // Mensaje de error

            }
        }




        public async Task<Result<bool>> DeleteAllById(GetRequest<T>? request = null)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();

                if (request != null)
                {
                    // Aplicar filtros
                    if (request.Filter != null)
                    {
                        query = query.Where(request.Filter);
                    }
                }

                // Obtener los registros que coinciden con el filtro
                var registros = await query.ToListAsync();

                if (!registros.Any())
                {
                    return Result<bool>.Failure("No se encontraron registros para eliminar.");
                }

                // Eliminar los registros
                _context.Set<T>().RemoveRange(registros);
                await _context.SaveChangesAsync();

                return Result<bool>.Success(true, "Registros eliminados exitosamente.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message); // Mensaje de error
            }
        }
    }
}

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

namespace Capa_Servicio.Services
{
    public class ProductoService : IProducto
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Producto> _repository;

        public ProductoService(IMapper mapper, IRepository<Producto> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        


        public async Task<Result<Productodto>> Add(Producto producto)
        {
            try
            {
                var resultado = await _repository.Add(producto);

                if (!resultado.IsSuccess)
                {
                    return Result<Productodto>.Failure(resultado.message);
                }
                else
                {
                    var productodto = _mapper.Map<Productodto>(resultado.Value);
                    return Result<Productodto>.Success(productodto, resultado.message);
                }
            }
            catch (Exception ex)
            {
                return Result<Productodto>.Failure(ex.Message);
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

        public async Task<Result<PaginatedListdto<Productodto>>> GetAll(int index, int take)
        {
            try
            {
                var resultado = await _repository.GetAll(null, index, take);

                if (!resultado.IsSuccess)
                {
                    return Result<PaginatedListdto<Productodto>>.Failure(resultado.message);
                }
                else
                {
                    var listadto = _mapper.Map<PaginatedListdto<Productodto>>(resultado.Value);
                    return Result<PaginatedListdto<Productodto>>.Success(listadto, resultado.message);
                }
            }
            catch (Exception ex)
            {
                return Result<PaginatedListdto<Productodto>>.Failure(ex.Message);
            }
        }

        public async Task<Result<Productodto>> GetById(int id)
        {
            try
            {
                var resultado = await _repository.GetById(id);

                if (!resultado.IsSuccess)
                {
                    return Result<Productodto>.Failure(resultado.message);
                }
                else
                {
                    var productodto = _mapper.Map<Productodto>(resultado.Value);
                    return Result<Productodto>.Success(productodto, resultado.message);
                }
            }
            catch (Exception ex)
            {
                return Result<Productodto>.Failure(ex.Message);
            }
        }

        public async Task<Result<Productodto>> Update(Producto producto)
        {
            try
            {
                var resultado = await _repository.Update(producto);

                if (!resultado.IsSuccess)
                {
                    return Result<Productodto>.Failure(resultado.message);
                }
                else
                {
                    var productodto = _mapper.Map<Productodto>(resultado.Value);
                    return Result<Productodto>.Success(productodto, resultado.message);
                }
            }
            catch (Exception ex)
            {
                return Result<Productodto>.Failure(ex.Message);
            }
        }



        
    }
}

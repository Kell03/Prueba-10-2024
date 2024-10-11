using AutoMapper;
using Capa_Acceso_Datos.Repository;
using Capa_Acceso_Datos.Utils;
using Capa_Servicio.Interface;
using CapaDominio;
using CapaDominio.Modeldto;
using CapaDominio.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Servicio.Services
{
    public class HabilidadService : IHabilidades
    {
        private readonly IMapper _mapper;
        private readonly  IRepository<Habilidades> _repository;

        public HabilidadService(IMapper mapper, IRepository<Habilidades> repository)
        {

            _mapper = mapper;
            _repository = repository;

        }
        public async Task<Result<Habilidades>> Add(Habilidades habilidad)
        {
            try
            {
                var request = new GetRequest<Habilidades>();
                //request.Filter = x => x.Id == 7;


                var resultado = await _repository.Add(habilidad);

                if (!resultado.IsSuccess)
                {
                    return Result<Habilidades>.Failure(resultado.message);
                }
                else
                {
                    var listadto = _mapper.Map<Habilidades>(resultado.Value);
                    return Result<Habilidades>.Success(listadto, resultado.message);

                }

            }
            catch (Exception ex)
            {

                return Result<Habilidades>.Failure(ex.Message);

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

        public async Task<Result<PaginatedListdto<Habilidadesdto>>> GetAll(int index, int take)
        {

            try
            {
                var request = new GetRequest<Habilidadesdto>();
                //request.Filter = x => x.Id == 7;


                var resultado = await _repository.GetAll(null, index,  take);

                if (!resultado.IsSuccess)
                {
                    return Result<PaginatedListdto<Habilidadesdto>>.Failure(resultado.message);
                }
                else
                {
                    var listadto = _mapper.Map<PaginatedListdto<Habilidadesdto>>(resultado.Value);
                    return Result<PaginatedListdto<Habilidadesdto>>.Success(listadto, resultado.message);

                }

            }
            catch (Exception ex)
            {

                return Result<PaginatedListdto<Habilidadesdto>>.Failure(ex.Message);

            }

        }

        public async Task<Result<Habilidadesdto>> GetById(int id)
        {
            try
            {
                var request = new GetRequest<Habilidadesdto>();
                //request.Filter = x => x.Id == 7;


                var resultado = await _repository.GetById(id);

                if (!resultado.IsSuccess)
                {
                    return Result<Habilidadesdto>.Failure(resultado.message);
                }
                else
                {
                    var listadto = _mapper.Map<Habilidadesdto>(resultado.Value);
                    return Result<Habilidadesdto>.Success(listadto, resultado.message);

                }

            }
            catch (Exception ex)
            {

                return Result<Habilidadesdto>.Failure(ex.Message);

            }
        }

        public async Task<Result<Habilidadesdto>> Update(Habilidades habilidad)
        {
            try
            {
               

                var resultado = await _repository.Update(habilidad);

                if (!resultado.IsSuccess)
                {
                    return Result<Habilidadesdto>.Failure(resultado.message);
                }
                else
                {
                    var listadto = _mapper.Map<Habilidadesdto>(resultado.Value);
                    return Result<Habilidadesdto>.Success(listadto, resultado.message);

                }

            }
            catch (Exception ex)
            {

                return Result<Habilidadesdto>.Failure(ex.Message);

            }
        }
    }
}

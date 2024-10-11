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
    public class UsuarioService: IUsuario
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Usuario> _repository;

        public UsuarioService(IMapper mapper, IRepository<Usuario> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Result<Usuariodto>> Add(Usuario usuario)
        {
            try
            {
                var resultado = await _repository.Add(usuario);

                if (!resultado.IsSuccess)
                {
                    return Result<Usuariodto>.Failure(resultado.message);
                }
                else
                {
                    var usuarioDto = _mapper.Map<Usuariodto>(resultado.Value);
                    return Result<Usuariodto>.Success(usuarioDto, resultado.message);
                }
            }
            catch (Exception ex)
            {
                return Result<Usuariodto>.Failure(ex.Message);
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

        public async Task<Result<PaginatedListdto<Usuariodto>>> GetAll(int index, int take)
        {
            try
            {
                var resultado = await _repository.GetAll(null, index, take);

                if (!resultado.IsSuccess)
                {
                    return Result<PaginatedListdto<Usuariodto>>.Failure(resultado.message);
                }
                else
                {
                    var listadto = _mapper.Map<PaginatedListdto<Usuariodto>>(resultado.Value);
                    return Result<PaginatedListdto<Usuariodto>>.Success(listadto, resultado.message);
                }
            }
            catch (Exception ex)
            {
                return Result<PaginatedListdto<Usuariodto>>.Failure(ex.Message);
            }
        }

        public async Task<Result<Usuariodto>> GetById(int id)
        {
            try
            {
                var resultado = await _repository.GetById(id);

                if (!resultado.IsSuccess)
                {
                    return Result<Usuariodto>.Failure(resultado.message);
                }
                else
                {
                    var usuarioDto = _mapper.Map<Usuariodto>(resultado.Value);
                    return Result<Usuariodto>.Success(usuarioDto, resultado.message);
                }
            }
            catch (Exception ex)
            {
                return Result<Usuariodto>.Failure(ex.Message);
            }
        }

        public async Task<Result<Usuariodto>> Update(Usuario usuario)
        {
            try
            {
                var resultado = await _repository.Update(usuario);

                if (!resultado.IsSuccess)
                {
                    return Result<Usuariodto>.Failure(resultado.message);
                }
                else
                {
                    var usuarioDto = _mapper.Map<Usuariodto>(resultado.Value);
                    return Result<Usuariodto>.Success(usuarioDto, resultado.message);
                }
            }
            catch (Exception ex)
            {
                return Result<Usuariodto>.Failure(ex.Message);
            }
        }
    }
}

using AutoMapper;
using Capa_Acceso_Datos.Utils;
using CapaDominio;
using CapaDominio.Modeldto;
using CapaDominio.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPractice.AutoMapper
{
    public class AutoMapper: Profile
    {

        public AutoMapper()
        {
            //contenedor para la paginacion
            CreateMap(typeof(PaginatedList<>), typeof(PaginatedListdto<>));

            #region Habilidades
            CreateMap<Habilidades, Habilidadesdto>();
            CreateMap<Habilidadesdto, Habilidades>();
            #endregion

            #region animal 
            CreateMap<Animales, Animalesdto>()
           .ForMember(dest => dest.Habilidad, opt => opt.MapFrom(src => src.Habilidad));

            CreateMap<Animalesdto, Animales>()
                .ForMember(destino => destino.Habilidad,
                opt => opt.Ignore());

            #endregion


            #region usuario

            CreateMap<Usuario, Usuariodto>();      // Mapeo de Usuario a Usuariodto
            CreateMap<Usuariodto, Usuario>();
            #endregion


            #region Producto 

            CreateMap<Producto, Productodto>();      // Mapeo de Usuario a Usuariodto
            CreateMap<Productodto, Producto>();
            #endregion

            #region Inventario 

           

            CreateMap<Inventario, Inventariodto>();
            CreateMap<Inventariodto, Inventario>()
                .ForMember(dest => dest.Producto, opt => opt.Ignore()); // Ignorar Producto en la entrada

            #endregion

            #region Auditoria
            // Mapeo de Auditoria a Auditoriadto, ignorando las propiedades de navegación
            CreateMap<Auditoria, Auditoriadto>();  

            // Mapeo de Auditoriadto a Auditoria
            CreateMap<Auditoriadto, Auditoria>()
                .ForMember(dest => dest.Producto, opt => opt.Ignore())  // Ignorar Producto
                .ForMember(dest => dest.Usuario, opt => opt.Ignore()); ;

            #endregion
        }

    }
    }

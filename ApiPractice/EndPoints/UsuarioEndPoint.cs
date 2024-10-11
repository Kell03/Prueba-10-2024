using AutoMapper;
using Capa_Servicio.Interface;
using CapaDominio;

namespace ApiPractice.EndPoints
{
    public static class UsuarioEndPoint
    {
        public static void RegisterUsuarioEndpoints(this IEndpointRouteBuilder routes)
        {
            // Agrupar los endpoints bajo "/usuario"
            var app = routes.MapGroup("/usuario");

            // Obtener una lista paginada de usuarios
            app.MapGet("/{index}/{take}", async (int index, int take, IUsuario service) =>
            {
                var lista = await service.GetAll(index, take);
                return lista;
            });

            // Obtener un usuario por ID
            app.MapGet("/{id}", async (int id, IUsuario service) =>
            {
                var usuario = await service.GetById(id);
                return usuario;
            });

            // Agregar un nuevo usuario
            app.MapPost("/", async (Usuariodto modeldto, IUsuario service, IMapper mapper) =>
            {
                    var model = mapper.Map<Usuario>(modeldto);
                    var resultado = await service.Add(model);
                    return resultado;
               
            });

            // Actualizar un usuario existente
            app.MapPut("/edit/{id}", async (int id, Usuariodto model, IUsuario service, IMapper _mapper) =>
            {

               
                    model.Id = id;
                    model.UpdatedAt = DateTime.Now; // Asegúrate de actualizar la fecha correctamente
                    var mappermodel = _mapper.Map<Usuario>(model);
                    var resultado = await service.Update(mappermodel);
                    return resultado;
                
            });

            // Eliminar un usuario por ID
            app.MapDelete("/delete/{id}", async (int id, IUsuario service) =>
            {
                var resultado = await service.Delete(id);
                return resultado;
            });
        }
    }
}

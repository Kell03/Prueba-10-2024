using AutoMapper;
using Capa_Servicio.Interface;
using CapaDominio;

namespace ApiPractice.EndPoints
{
    public static class AuditoriaEndPoint
    {
        public static void RegisterAuditoriaEndpoints(this IEndpointRouteBuilder routes)
        {
            // Agrupar los endpoints bajo "/auditoria"
            var app = routes.MapGroup("/auditoria");

            // Obtener una lista paginada de registros de auditoría
            app.MapGet("/{index}/{take}", async (int index, int take, IAuditoria service) =>
            {
                var lista = await service.GetAll(index, take);
                return lista;
            });

            // Obtener un registro de auditoría por ID
            app.MapGet("/{id}", async (int id, IAuditoria service) =>
            {
                var auditoria = await service.GetById(id);
                return auditoria;
            });


            // Obtener un registro de inventario por ID
            app.MapGet("/lastproduct/{id}", async (int id, IAuditoria
                service) =>
            {
                var auditoria = await service.getExitsProduct(id);
                return auditoria;
            });


            // Agregar un nuevo registro de auditoría
            app.MapPost("/", async (Auditoriadto modeldto, IAuditoria service, IMapper mapper) =>
            {
                try
                {
                    var model = mapper.Map<Auditoria>(modeldto);
                    var resultado = await service.Add(model);
                    return resultado;
                }
                catch (Exception ex)
                {
                    return null; // Manejo de errores simple
                }
            });

            // Actualizar un registro de auditoría existente
            app.MapPut("/edit/{id}", async (int id, Auditoriadto model, IAuditoria service, IMapper _mapper) =>
            {
                model.Id = id; // Asignar el ID para la actualización
                model.UpdatedAt = DateTime.Now; // Actualizar la fecha
                var mappermodel = _mapper.Map<Auditoria>(model);
                var resultado = await service.Update(mappermodel);
                return resultado;
            });

            // Eliminar un registro de auditoría por ID
            app.MapDelete("/delete/{id}", async (int id, IAuditoria service) =>
            {
                var resultado = await service.Delete(id);
                return resultado;
            });
        }

    }
}

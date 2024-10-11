using AutoMapper;
using Capa_Servicio.Interface;
using CapaDominio;

namespace ApiPractice.EndPoints
{
    public static class InventarioEndPoint
    {
        public static void RegisterInventarioEndpoints(this IEndpointRouteBuilder routes)
        {
            // Agrupar los endpoints bajo "/inventario"
            var app = routes.MapGroup("/inventario");

            // Obtener una lista paginada de registros de inventario
            app.MapGet("/{index}/{take}", async (int index, int take, IInventario service) =>
            {
                var lista = await service.GetAll(index, take);
                return lista;
            });

            // Obtener un registro de inventario por ID
            app.MapGet("/{id}", async (int id, IInventario service) =>
            {
                var inventario = await service.GetById(id);
                return inventario;
            });


            // Obtener un registro de inventario por ID
            app.MapGet("/lastproduct/{id}", async (int id, IInventario service) =>
            {
                var inventario = await service.getExitsProduct(id);
                return inventario;
            });


            // eliminar todos los productos del inventario por ID
            app.MapDelete("/deleteproducts/{id}", async (int id, IInventario service) =>
            {
                var inventario = await service.DeleteAllById(id);
                return inventario;
            });

            // Agregar un nuevo registro de inventario
            app.MapPost("/", async (Inventariodto modeldto, IInventario service, IMapper mapper) =>
            {
                try
                {
                    var model = mapper.Map<Inventario>(modeldto);
                    var resultado = await service.Add(model);
                    return resultado;
                }catch(Exception ex)
                {
                    return null;
                }
            });

            // Actualizar un registro de inventario existente
            app.MapPut("/edit/{id}", async (int id, Inventariodto model, IInventario service, IMapper _mapper) =>
            {
                model.Id = id;
                model.UpdatedAt = DateTime.Now; // Asegúrate de actualizar la fecha correctamente
                var mappermodel = _mapper.Map<Inventario>(model);
                var resultado = await service.Update(mappermodel);
                return resultado;
            });

            // Eliminar un registro de inventario por ID
            app.MapDelete("/delete/{id}", async (int id, IInventario service) =>
            {
                var resultado = await service.Delete(id);
                return resultado;
            });
        }
    }
}

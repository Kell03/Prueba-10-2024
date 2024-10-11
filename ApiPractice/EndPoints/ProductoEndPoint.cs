using AutoMapper;
using Capa_Servicio.Interface;
using CapaDominio;

namespace ApiPractice.EndPoints
{
    public static class ProductoEndPoint
    {
        public static void RegisterProductoEndpoints(this IEndpointRouteBuilder routes)
        {
            // Agrupar los endpoints bajo "/producto"
            var app = routes.MapGroup("/producto");

            // Obtener una lista paginada de productos
            app.MapGet("/{index}/{take}", async (int index, int take, IProducto service) =>
            {
                var lista = await service.GetAll(index, take);
                return lista;
            });

            // Obtener un producto por ID
            app.MapGet("/{id}", async (int id, IProducto service) =>
            {
                var producto = await service.GetById(id);
                return producto;
            });

            // Agregar un nuevo producto
            app.MapPost("/", async (Productodto modeldto, IProducto service, IMapper mapper) =>
            {
                var model = mapper.Map<Producto>(modeldto);
                var resultado = await service.Add(model);
                return resultado;
            });

            // Actualizar un producto existente
            app.MapPut("/edit/{id}", async (int id, Productodto model, IProducto service, IMapper _mapper) =>
            {
                model.Id = id;
                model.UpdatedAt = DateTime.Now; // Asegúrate de actualizar la fecha correctamente
                var mappermodel = _mapper.Map<Producto>(model);
                var resultado = await service.Update(mappermodel);
                return resultado;
            });

            // Eliminar un producto por ID
            app.MapDelete("/delete/{id}", async (int id, IProducto service) =>
            {
                var resultado = await service.Delete(id);
                return resultado;
            });
        }
    }
}

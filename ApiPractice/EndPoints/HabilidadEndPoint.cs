using AutoMapper;
using Capa_Servicio.Interface;
using CapaDominio;
using CapaDominio.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPractice.EndPoints
{
    public static class HabilidadEndPoint
    {
        public static void RegisterHabilidadEndpoints(this IEndpointRouteBuilder routes)
        {
            // Grouped endpoint will go here

            var app = routes.MapGroup("/habilidad");



            app.MapGet("/{index}/{take}", async (int index, int take, IHabilidades service) =>
            {
                // Aquí puedes utilizar el servicio
                var lista = await service.GetAll(index, take);
                //  var lista = get.Value;


                return lista;
            });


            app.MapGet("/{id}", async (int id, IHabilidades service) =>
            {


                var lista = await service.GetById(id);

                return lista;


            });


            app.MapPost("/", async (Habilidadesdto modeldto, IHabilidades service, IMapper mapper) =>

            {

                var model = mapper.Map<Habilidades>(modeldto);
                    var _model = await service.Add(model);



                    return _model;
            



            });


            app.MapPut("/edit/{id}", async (int id, Habilidadesdto model, IHabilidades service, IMapper _mapper) =>
            {

                    model.Id = id;
                    model.UpdatedAt = DateTime.Now;
                  
                        var mappermodel = _mapper.Map<Habilidades>(model);
                        var edit = await service.Update(mappermodel);
                        return edit;
                 
                
              
            });



            app.MapDelete("/delete/{id}", async (int id, IHabilidades service) =>
            {


                var lista = await service.Delete(id);

                return lista;

            });



        }
    }
}
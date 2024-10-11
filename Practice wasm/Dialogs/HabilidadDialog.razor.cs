using Microsoft.AspNetCore.Components;
using Practice_wasm.Interface;
using Practice_wasm.Models;
using Practice_wasm.Utils;
using Radzen;

namespace Practice_wasm.Dialogs
{

    public partial class HabilidadDialog: ComponentBase
    {

        [Parameter] public int Id { get; set; }


        [Parameter] public EventCallback<bool> OnSaved { get; set; } // Evento para notificar al componente padre

        [Inject]
        public IHabilidadService HabilidadService { get; set; }

        private HabilidadDto habilidad = new HabilidadDto(); // Propiedad para almacenar el resultado


        protected override async Task OnInitializedAsync()
        {
            try
            {
                // Realiza la solicitud al servicio para obtener el HabilidadDto por Id
                if (Id > 0)
                {
                    var respuesta = await HabilidadService.GetById(Id);

                    if (respuesta.IsSuccess)
                    {
                        habilidad = respuesta.Value; // Asigna el resultado a la propiedad
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción
                Console.WriteLine($"Error al cargar la habilidad: {ex.Message}");
            }
        }



        private async Task ActualizarHabilidad()
        {
            try
            {
                var result = new Result<HabilidadDto>();
                var message = "";

                // Llama a tu servicio para actualizar la habilidad
                if (Id > 0)
                {
                    result = await HabilidadService.Update(habilidad.Id, habilidad);
                    message = "Registro actualizado con éxito.";

                }
                else
                {
                     result = await HabilidadService.Add( habilidad);
                    message = "Registro guardado con éxito.";


                }

                if (result.IsSuccess) // Verifica que la actualización fue exitosa
                {

                    await OnSaved.InvokeAsync(true); // Notifica al componente padre
                     dialogService.Close(true); // Cierra el diálogo
                    await dialogService.Alert(message, "Éxito");
                }

            }
            catch (Exception ex)
            {
                // Manejar errores (puedes mostrar un mensaje de error)
                await dialogService.Alert($"Errors: {ex.Message}", "Error");
            }
        }



        private async Task CerrarDialogo()
        {
            // Aquí puedes agregar lógica adicional si es necesario
             dialogService.Close(); // Cierra el diálogo de forma asíncrona
        }


    }
}

using Microsoft.AspNetCore.Components;
using Practice_wasm.Dialogs;
using Practice_wasm.Interface;
using Practice_wasm.Models;
using Practice_wasm.Services;
using Radzen;
using Radzen.Blazor;

namespace Practice_wasm.Pages
{
    public partial class Habilidades: ComponentBase
    {
           IQueryable<HabilidadDto> habilidadlist;
        
          IList<HabilidadDto> selectedhabilidad;

        [Inject]
        public IHabilidadService HabilidadService { get; set; } // Inyección del servicio

        // Constructor

        private RadzenDataGrid<HabilidadDto> ordersGrid; // Define la referencia

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await CargarHabilidades();
            }
            catch (Exception ex)
            {

                // Manejar la excepción
            }
        }


        private async Task CargarHabilidades()
        {

            var respuesta = await HabilidadService.GetAll();
            habilidadlist = respuesta.Value.Data.AsQueryable(); // Convierto la lista a IQueryable
            selectedhabilidad = new List<HabilidadDto>() { habilidadlist.FirstOrDefault() };

        }

        private void OnActionClick(HabilidadDto habilidad)
        {
            // Manejar la acción al hacer clic en el botón
            Console.WriteLine($"Botón clickeado para habilidad: {habilidad.Nombre}");
        }


        async Task OpenOrder(int orderId)
        {
            try
            {

                var dialog = await DialogService.OpenAsync<HabilidadDialog>($"Order {orderId}",
                      new Dictionary<string, object>() { { "Id", orderId } },
                      new DialogOptions() { Width = "50%", Height = "60%" });



                if (dialog) // Verifica que el diálogo no fue cancelado
                {
                    await CargarHabilidades(); // Actualiza la lista después de agregar
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);   
            }


        }

        private async Task Eliminar(int id)
        {
            var confirmResult = await DialogService.Confirm("¿Estás seguro de que deseas eliminar este elemento?", "Confirmación", new ConfirmOptions() { OkButtonText = "Sí", CancelButtonText = "No" });

            if (confirmResult.HasValue && confirmResult.Value)
            {
               var result = await HabilidadService.Delete(id); // Llama al servicio para eliminar
                
                if (result.IsSuccess)
                {
                   await  CargarHabilidades();
                }
            }

        }
    }
}

using Microsoft.AspNetCore.Components;
using Practice_wasm.Interface;
using Practice_wasm.Models;
using Radzen;
using Radzen.Blazor;

namespace Practice_wasm.Pages
{
    public partial class InventarioPage : ComponentBase
    {
        IQueryable<InventarioDto> inventarioList; // Cambiado a InventarioDto
        IList<InventarioDto> selectedInventario; // Cambiado a InventarioDto

        [Inject]
        public IInventarioService InventarioService { get; set; } // Inyección del servicio

        // Constructor
        private RadzenDataGrid<InventarioDto> inventariosGrid; // Define la referencia

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await CargarInventarios(); // Cargar la lista de inventarios al inicializar
            }
            catch (Exception ex)
            {
                // Manejar la excepción
                Console.WriteLine(ex.Message);
            }
        }

        private async Task CargarInventarios()
        {
            var respuesta = await InventarioService.GetAll(); // Cambiado a InventarioService
            inventarioList = respuesta.Value.Data.AsQueryable(); // Convierto la lista a IQueryable
            Console.WriteLine("dddd", selectedInventario);
            selectedInventario = new List<InventarioDto>() { inventarioList.FirstOrDefault() }; // Asigno el primer inventario como seleccionado
            var t = inventarioList.FirstOrDefault();
            Console.Write("dddd", selectedInventario);


        }

        private void OnActionClick(InventarioDto inventario) // Cambiado a InventarioDto
        {
            // Manejar la acción al hacer clic en el botón
            Console.WriteLine($"Botón clickeado para inventario: {inventario.ProductoId}"); // Puedes mostrar otra propiedad que desees
        }

        async Task OpenOrder(int inventarioId) // Cambiado a inventarioId
        {
           // try
           // {
           //     var dialog = await DialogService.OpenAsync<InventarioDialog>($"Inventario {inventarioId}", // Cambiado a InventarioDialog
           //           new Dictionary<string, object>() { { "Id", inventarioId } },
           //           new Radzen.DialogOptions() { Width = "50%", Height = "60%" });
           //
           //     if (dialog) // Verifica que el diálogo no fue cancelado
           //     {
           //         await CargarInventarios(); // Actualiza la lista después de agregar
           //     }
           // }
           // catch (Exception ex)
           // {
           //     Console.WriteLine(ex.Message);
           // }
        }

        private async Task Eliminar(int id)
        {
            var confirmResult = await DialogService.Confirm("¿Estás seguro de que deseas eliminar este registro de inventario?", "Confirmación", new ConfirmOptions() { OkButtonText = "Sí", CancelButtonText = "No" });

            if (confirmResult.HasValue && confirmResult.Value)
            {
                var result = await InventarioService.Delete(id); // Llama al servicio para eliminar el inventario

                if (result.IsSuccess)
                {
                    await CargarInventarios(); // Recarga la lista de inventarios después de la eliminación
                }
            }
        }
    }
}

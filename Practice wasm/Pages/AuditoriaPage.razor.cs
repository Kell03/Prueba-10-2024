using Microsoft.AspNetCore.Components;
using Practice_wasm.Interface;
using Practice_wasm.Models;
using Radzen.Blazor;

namespace Practice_wasm.Pages
{
    public partial class AuditoriaPage : ComponentBase
    {

        IQueryable<AuditoriaDto> auditoriaList; // Cambiado a AuditoriaDto
        IList<AuditoriaDto> selectedAuditoria; // Cambiado a AuditoriaDto

        [Inject]
        public IAuditoriaService AuditoriaService { get; set; } // Inyección del servicio

        // Constructor
        private RadzenDataGrid<AuditoriaDto> auditoriasGrid; // Define la referencia

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await CargarAuditorias(); // Cargar la lista de auditorías al inicializar
            }
            catch (Exception ex)
            {
                // Manejar la excepción
                Console.WriteLine(ex.Message);
            }
        }

        private async Task CargarAuditorias()
        {
            var respuesta = await AuditoriaService.GetAll(); // Cambiado a AuditoriaService
            auditoriaList = respuesta.Value.Data.AsQueryable(); // Convierto la lista a IQueryable
            selectedAuditoria = new List<AuditoriaDto>() { auditoriaList.FirstOrDefault() }; // Asigno el primer registro como seleccionado
            Console.WriteLine(selectedAuditoria);
        }

        private void OnActionClick(AuditoriaDto auditoria) // Cambiado a AuditoriaDto
        {
            // Manejar la acción al hacer clic en el botón
            Console.WriteLine($"Botón clickeado para auditoría: {auditoria.ProductoId}"); // Puedes mostrar otra propiedad que desees
        }

        async Task OpenOrder(int auditoriaId) // Cambiado a auditoriaId
        {
            // Implementación del diálogo
        }

        private async Task Eliminar(int id)
        {
          //var confirmResult = await DialogService.Confirm("¿Estás seguro de que deseas eliminar este registro de auditoría?", "Confirmación", new ConfirmOptions() { OkButtonText = "Sí", CancelButtonText = "No" });
          //
          //if (confirmResult.HasValue && confirmResult.Value)
          //{
          //    var result = await AuditoriaService.Delete(id); // Llama al servicio para eliminar la auditoría
          //
          //    if (result.IsSuccess)
          //    {
          //        await CargarAuditorias(); // Recarga la lista de auditorías después de la eliminación
          //    }
          //}
        }
    }
}

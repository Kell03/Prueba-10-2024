using Microsoft.AspNetCore.Components;
using Practice_wasm.Dialogs;
using Practice_wasm.Interface;
using Practice_wasm.Models;
using Radzen;
using Radzen.Blazor;

namespace Practice_wasm.Pages
{
    public partial class UsuarioPage:ComponentBase
    {

        IQueryable<UsuarioDto> usuarioList;

        IList<UsuarioDto> selectedUsuario;

        [Inject]
        public IUsuarioService UsuarioService { get; set; } // Inyección del servicio

        // Constructor

        private RadzenDataGrid<UsuarioDto> usersGrid; // Define la referencia

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await CargarUsuarios(); // Cargar la lista de usuarios al inicializar
            }
            catch (Exception ex)
            {
                // Manejar la excepción
                Console.WriteLine(ex.Message);
            }
        }

        private async Task CargarUsuarios()
        {
            var respuesta = await UsuarioService.GetAll();
            usuarioList = respuesta.Value.Data.AsQueryable(); // Convierto la lista a IQueryable
            selectedUsuario = new List<UsuarioDto>() { usuarioList.FirstOrDefault() }; // Asigno el primer usuario como seleccionado
        }

        private void OnActionClick(UsuarioDto usuario)
        {
            // Manejar la acción al hacer clic en el botón
            Console.WriteLine($"Botón clickeado para usuario: {usuario.NombreUsuario}");
        }

        async Task OpenOrder(int userId)
        {
           try
           {
               var dialog = await DialogService.OpenAsync<UsuarioDialog>($"Order {userId}",
                     new Dictionary<string, object>() { { "Id", userId } },
                     new DialogOptions() { Width = "50%", Height = "60%" });
          
               if (dialog) // Verifica que el diálogo no fue cancelado
               {
                   await CargarUsuarios(); // Actualiza la lista después de agregar
               }
           }
           catch (Exception ex)
           {
               Console.WriteLine(ex.Message);
           }
        }

        private async Task Eliminar(int id)
        {
            var confirmResult = await DialogService.Confirm("¿Estás seguro de que deseas eliminar este usuario?", "Confirmación", new ConfirmOptions() { OkButtonText = "Sí", CancelButtonText = "No" });

            if (confirmResult.HasValue && confirmResult.Value)
            {
                var result = await UsuarioService.Delete(id); // Llama al servicio para eliminar el usuario

                if (result.IsSuccess)
                {
                    await CargarUsuarios(); // Recarga la lista de usuarios después de la eliminación
                }
            }
        }





    }
}

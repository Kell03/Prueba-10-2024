using Microsoft.AspNetCore.Components;
using Practice_wasm.Interface;
using Practice_wasm.Models;
using Practice_wasm.Utils;

namespace Practice_wasm.Dialogs
{
    public partial class UsuarioDialog: ComponentBase
    {

        [Parameter] public int Id { get; set; }

        [Parameter] public EventCallback<bool> OnSaved { get; set; } // Evento para notificar al componente padre

        [Inject]
        public IUsuarioService UsuarioService { get; set; }

        private UsuarioDto usuario = new UsuarioDto(); // Propiedad para almacenar el resultado

        protected override async Task OnInitializedAsync()
        {
            try
            {
                // Realiza la solicitud al servicio para obtener el UsuarioDto por Id
                if (Id > 0)
                {
                    var respuesta = await UsuarioService.GetById(Id);

                    if (respuesta.IsSuccess)
                    {
                        usuario = respuesta.Value; // Asigna el resultado a la propiedad
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción
                Console.WriteLine($"Error al cargar el usuario: {ex.Message}");
            }
        }

        private async Task ActualizarUsuario()
        {
            try
            {
                var result = new Result<UsuarioDto>();
                var message = "";

                // Llama a tu servicio para actualizar el usuario
                if (Id > 0)
                {
                    result = await UsuarioService.Update(usuario.Id, usuario);
                    message = "Usuario actualizado con éxito.";
                }
                else
                {
                    result = await UsuarioService.Add(usuario);
                    message = "Usuario guardado con éxito.";
                }

                if (result.IsSuccess) // Verifica que la actualización o creación fue exitosa
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

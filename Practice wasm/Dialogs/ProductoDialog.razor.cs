using Microsoft.AspNetCore.Components;
using Practice_wasm.Interface;
using Practice_wasm.Models;
using Practice_wasm.Utils;

namespace Practice_wasm.Dialogs
{
    public partial class ProductoDialog : ComponentBase
    {



        [Parameter] public int Id { get; set; }

        [Parameter] public EventCallback<bool> OnSaved { get; set; } // Evento para notificar al componente padre

        [Inject]
        public IProductoService ProductoService { get; set; }

        [Inject]
        public IInventarioService InventarioService { get; set; } // Inyección del servicio
        [Inject]
        public IAuditoriaService AuditoriaService { get; set; } // Inyección del servicio



        private ProductoDto producto = new ProductoDto(); // Modelo para almacenar el resultado

        private List<SelectListItem> tipoElaboracionOptions = new List<SelectListItem>
    {
        new SelectListItem { Text = "Elaborado a mano", Value = "Elaborado a mano" },
        new SelectListItem { Text = "Elaborado a mano y maquina", Value = "Elaborado a mano y maquina" }
    };

        private List<SelectListItem> tipoestadoproducto = new List<SelectListItem>
    {
        new SelectListItem { Text = "Optimo", Value = "Optimo" },
        new SelectListItem { Text = "Defectuoso", Value = "Defectuoso" }
    };


        protected override async Task OnInitializedAsync()
        {
            try
            {

                if (Id > 0)
                {

                    var respuesta = await ProductoService.GetById(Id);

                    if (respuesta.IsSuccess)
                    {
                        producto = respuesta.Value;



                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar el producto: {ex.Message}");
            }
        }




        private async Task ActualizarProducto()
        {
            try
            {
                var result = new Result<ProductoDto>();
                var message = "";

                // Llama a tu servicio para actualizar el producto
                if (Id > 0)
                {
                    result = await ProductoService.Update(producto.Id, producto);
                    if (result.IsSuccess)
                    {
                        await InventarioService.Update(result.Value);
                        await AuditoriaService.Update(result.Value);
                    }
                    message = "Producto actualizado con éxito.";
                }
                else
                {
                    result = await ProductoService.Add(producto);
                    if (result.IsSuccess)
                    {

                        await InventarioService.Add(result.Value);
                        await AuditoriaService.Add(result.Value, "Compra");


                    }
                    message = "Producto guardado con éxito.";
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
            dialogService.Close(); // Cierra el diálogo de forma asíncrona
        }

        private void OnTipoElaboracionChange(object args)
        {
            producto.TipoElaboracion = args.ToString(); // Actualiza el valor en el modelo
        }

        private void OnestadoproductoChange(object args)
        {
            producto.Estado = args.ToString(); // Actualiza el valor en el modelo
        }
    }
}

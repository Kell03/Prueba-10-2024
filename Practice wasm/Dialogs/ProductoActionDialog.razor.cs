using Microsoft.AspNetCore.Components;
using Practice_wasm.Interface;
using Practice_wasm.Models;

namespace Practice_wasm.Dialogs
{
    public partial class ProductoActionDialog: ComponentBase
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

        public string accion = "";
        public int contador= 0;


        private List<SelectListItem> tipoaccionOptions = new List<SelectListItem>
    {
        new SelectListItem { Text = "Venta", Value = "Venta" },
        new SelectListItem { Text = "Devolucion", Value = "Devolucion" }
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


        private void OnaccionChange(object args)
        {
            accion = args.ToString(); // Actualiza el valor en el modelo
        }


        private void IncrementCount()
        {
            if( contador >= 0 && contador < producto.CantidadDisponible)
            {
                contador++;

            }
            
        }


        private void DecrementCount()
        {
            if (contador >= 0 && contador <= producto.CantidadDisponible)
            {
                contador--;

            }

        }


        private async Task MakeTransaction()
        {
            try
            {
                // Asegúrate de que el contador no sea cero
                if (contador <= 0)
                {
                    await dialogService.Alert("La cantidad Debe Ser mayor a cero", "Error");
                    return;
                }

                // Actualiza la cantidad del producto
                producto.CantidadDisponible -= contador;

                // Realiza la transacción llamando al servicio
                var result = await ProductoService.Update(producto.Id, producto);

                if (result.IsSuccess)
                {
                    await InventarioService.Update(result.Value);
                    await AuditoriaService.Add(result.Value, accion, contador);
                    // Notificar al componente padre si es necesario
                    await OnSaved.InvokeAsync(true);
                    dialogService.Close(true); // Cierra el diálogo
                    var message = $"{accion} realizada con éxito.";
                    await dialogService.Alert(message, "Éxito");
                }
                else
                {
                    Console.WriteLine($"Error al realizar la transacción: {result.message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la transacción: {ex.Message}");
            }
        }


        private async Task CerrarDialogo()
        {
            dialogService.Close(); // Cierra el diálogo de forma asíncrona
        }

    }
}

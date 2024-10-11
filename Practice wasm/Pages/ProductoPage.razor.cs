using Microsoft.AspNetCore.Components;
using Practice_wasm.Dialogs;
using Practice_wasm.Interface;
using Practice_wasm.Models;
using Radzen;
using Radzen.Blazor;

namespace Practice_wasm.Pages
{
    public partial class ProductoPage: ComponentBase
    {

        IQueryable<ProductoDto> productoList;

        IList<ProductoDto> selectedProducto;

        [Inject]
        public IProductoService ProductoService { get; set; } // Inyección del servicio

        [Inject]
        public IInventarioService InventarioService { get; set; } // Inyección del servicio




        // Constructor

        private RadzenDataGrid<ProductoDto> productsGrid; // Define la referencia

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await CargarProductos(); // Cargar la lista de productos al inicializar
            }
            catch (Exception ex)
            {
                // Manejar la excepción
                Console.WriteLine(ex.Message);
            }
        }

        private async Task CargarProductos()
        {
            var respuesta = await ProductoService.GetAll();
            productoList = respuesta.Value.Data.AsQueryable(); // Convierto la lista a IQueryable
            selectedProducto = new List<ProductoDto>() { productoList.FirstOrDefault() }; // Asigno el primer producto como seleccionado
        }

        private void OnActionClick(ProductoDto producto)
        {
            // Manejar la acción al hacer clic en el botón
            Console.WriteLine($"Botón clickeado para producto: {producto.Nombre}");
        }

        async Task OpenOrder(int productId)
        {
            try
            {
                var dialog = await DialogService.OpenAsync<ProductoDialog>($"Producto {productId}",
                      new Dictionary<string, object>() { { "Id", productId } },
                      new Radzen.DialogOptions() { Width = "50%", Height = "60%" });
           
                if (dialog) // Verifica que el diálogo no fue cancelado
                {
                    await CargarProductos(); // Actualiza la lista después de agregar
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        
        async Task Maketransaction(int productId)
        {
            try
            {
                var dialog = await DialogService.OpenAsync<ProductoActionDialog>($"Transaccion",
                      new Dictionary<string, object>() { { "Id", productId } },
                      new Radzen.DialogOptions() { Width = "40%", Height = "40%" });
           
                if (dialog) // Verifica que el diálogo no fue cancelado
                {
                    await CargarProductos(); // Actualiza la lista después de agregar
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task Eliminar(int id)
        {
            var confirmResult = await DialogService.Confirm("¿Estás seguro de que deseas eliminar este producto?", "Confirmación", new ConfirmOptions() { OkButtonText = "Sí", CancelButtonText = "No" });
          
            if (confirmResult.HasValue && confirmResult.Value)
            {
                var confirmResult2 = await DialogService.Confirm("Si realiza esta acción, todos los productos serán eliminados del inventario. ¿Está seguro?", "Confirmación", new ConfirmOptions() { OkButtonText = "Sí", CancelButtonText = "No" });

                if (confirmResult2.HasValue && confirmResult2.Value)
                {
                    await InventarioService.Delete(id);

                    var result = await ProductoService.Delete(id); // Llama al servicio para eliminar el producto

                    if (result.IsSuccess)
                    {
                        await CargarProductos(); // Recarga la lista de productos después de la eliminación
                    }
                }
            }
        }

    }
}

namespace Practice_wasm.Models
{
    public class InventarioDto
    {
       
            public int Id { get; set; }

            public int ProductoId { get; set; }

            public int Cantidad { get; set; }

            public string TipoMovimiento { get; set; } = null!;

            public DateTime FechaMovimiento { get; set; }

            public DateTime CreatedAt { get; set; }

            public DateTime? UpdatedAt { get; set; }

            public  ProductoDto Producto { get; set; }
        
    }
}

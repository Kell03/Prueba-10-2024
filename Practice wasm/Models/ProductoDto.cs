namespace Practice_wasm.Models
{
    public partial class ProductoDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string TipoElaboracion { get; set; } = null!;

        public string Estado { get; set; } = null!;

        public int CantidadDisponible { get; set; }

        public int? Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }


    }

}

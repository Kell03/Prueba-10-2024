namespace Practice_wasm.Models
{
    public class HabilidadDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public int? Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

    }
}

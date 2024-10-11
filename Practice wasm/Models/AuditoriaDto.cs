namespace Practice_wasm.Models
{
    public class AuditoriaDto
    {

        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public int? ProductoId { get; set; }

        public int? Cantidad { get; set; }

        public string Accion { get; set; } = null!;

        public string Motivo { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual ProductoDto? Producto { get; set; }

        public virtual UsuarioDto Usuario { get; set; } = null!;
    }
}

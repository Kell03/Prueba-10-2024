namespace Practice_wasm.Models
{
    public partial class UsuarioDto
    {
        public int Id { get; set; }

        public string NombreUsuario { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;


        public string Password { get; set; } = null!;

        public int Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

    }

}

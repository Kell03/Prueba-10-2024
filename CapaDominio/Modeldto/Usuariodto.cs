using System;
using System.Collections.Generic;

namespace CapaDominio;

public partial class Usuariodto
{
    public int Id { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

}

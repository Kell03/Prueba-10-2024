using System;
using System.Collections.Generic;

namespace CapaDominio;

public partial class Auditoria
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public int? ProductoId { get; set; }

    public int? Cantidad { get; set; }

    public string Accion { get; set; } = null!;

    public string Motivo { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Producto? Producto { get; set; }

    public virtual Usuario? Usuario { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace CapaDominio;

public partial class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string TipoElaboracion { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public int CantidadDisponible { get; set; }

    public int? Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Auditoria> Auditoria { get; set; } = new List<Auditoria>();

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}

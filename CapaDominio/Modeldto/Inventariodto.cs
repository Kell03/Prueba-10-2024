using CapaDominio;
using System;
using System.Collections.Generic;

namespace CapaDominio;

public partial class Inventariodto
{
    public int Id { get; set; }

    public int ProductoId { get; set; }

    public int Cantidad { get; set; }

    public string TipoMovimiento { get; set; } = null!;

    public DateTime FechaMovimiento { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Productodto? Producto { get; set; } = null!;
}

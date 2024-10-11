using System;
using System.Collections.Generic;

namespace CapaDominio.Models;

public partial class Habilidades
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Animales> Animales { get; set; } = new List<Animales>();
}

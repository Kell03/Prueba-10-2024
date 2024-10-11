using System;
using System.Collections.Generic;

namespace CapaDominio;

public partial class Habilidadesdto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    //public virtual ICollection<Animalesdto> Animales { get; set; } = new List<Animalesdto>();
}

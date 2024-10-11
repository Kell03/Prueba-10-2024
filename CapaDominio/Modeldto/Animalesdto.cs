using System;
using System.Collections.Generic;

namespace CapaDominio;

public partial class Animalesdto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Especie { get; set; } = null!;

    public string Habitat { get; set; } = null!;

    public int? Status { get; set; }

    public int? HabilidadId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Habilidadesdto? Habilidad { get; set; }
}

using System;
using System.Collections.Generic;

namespace Projektni_zadatak___RWA.Models;

public partial class Proizvodi
{
    public int Id { get; set; }

    public string? Naziv { get; set; }

    public string? Opis { get; set; }

    public decimal? Cijena { get; set; }

    public string? SlikaUrl { get; set; }

    public virtual ICollection<Korpa> Korpas { get; } = new List<Korpa>();
}

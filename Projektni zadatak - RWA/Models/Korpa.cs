using System;
using System.Collections.Generic;

namespace Projektni_zadatak___RWA.Models;

public partial class Korpa
{
    public int Id { get; set; }

    public int? ProizvodId { get; set; }

    public int Kolicina { get; set; }

    public virtual Proizvodi? Proizvod { get; set; }
}

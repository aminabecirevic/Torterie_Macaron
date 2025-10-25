using System;
using System.Collections.Generic;

namespace Projektni_zadatak___RWA.Models;

public partial class Korisnici
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Lozinka { get; set; } = null!;
}

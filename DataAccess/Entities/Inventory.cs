using System;
using System.Collections.Generic;

namespace Labb2_DbFirst_Template.Entities;

public partial class Inventory
{
    public int StoresId { get; set; }

    public string Isbn13id { get; set; } = null!;

    public int Amount { get; set; }

    public virtual Book Isbn13 { get; set; } = null!;

    public virtual Store Stores { get; set; } = null!;
}

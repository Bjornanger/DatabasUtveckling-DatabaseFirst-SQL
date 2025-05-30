using System;
using System.Collections.Generic;

namespace DataAccess.Entities.ViewsFromDB;

public partial class VTitlesPerAuthor
{
    public string Name { get; set; } = null!;

    public string Age { get; set; } = null!;

    public int? BooksSt { get; set; }

    public string StockValue { get; set; } = null!;
}

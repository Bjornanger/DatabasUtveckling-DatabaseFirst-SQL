using System;
using System.Collections.Generic;

namespace DataAccess.Entities.ViewsFromDB;

public partial class VOrderReciept
{
    public string? OrderId { get; set; }

    public string IdStore { get; set; } = null!;

    public string IdClerkName { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Price { get; set; } = null!;

    public string CustomerInfo { get; set; } = null!;

    public string TotalSum { get; set; } = null!;

    public string? OrderDate { get; set; }
}

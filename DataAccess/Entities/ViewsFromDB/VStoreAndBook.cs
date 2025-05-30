using System;
using System.Collections.Generic;

namespace DataAccess.Entities.ViewsFromDB;

public partial class VStoreAndBook
{
    public int StoreId { get; set; }

    public string StoreName { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Isbn { get; set; } = null!;

    public int? Quantity { get; set; }
}

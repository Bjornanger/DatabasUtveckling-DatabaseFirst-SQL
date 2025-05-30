using System;
using System.Collections.Generic;

namespace Labb2_DbFirst_Template.Entities;

public partial class BookOrder
{
    public int OrderNumberId { get; set; }

    public DateTime OrderDateTime { get; set; }

    public int? StoreId { get; set; }

    public int? EmployeeId { get; set; }

    public int? CustomerId { get; set; }

    public string? BookIsbnid { get; set; }

    public int? Quantity { get; set; }

    public virtual Book? BookIsbn { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Store? Store { get; set; }
}

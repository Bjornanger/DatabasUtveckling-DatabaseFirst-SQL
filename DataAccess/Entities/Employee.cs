using System;
using System.Collections.Generic;

namespace Labb2_DbFirst_Template.Entities;

public partial class Employee
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int? StoreId { get; set; }

    public virtual ICollection<BookOrder> BookOrders { get; set; } = new List<BookOrder>();

    public virtual Store? Store { get; set; }
}

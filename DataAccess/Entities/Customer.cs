using System;
using System.Collections.Generic;

namespace Labb2_DbFirst_Template.Entities;

public partial class Customer
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<BookOrder> BookOrders { get; set; } = new List<BookOrder>();
}

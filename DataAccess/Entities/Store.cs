using System;
using System.Collections.Generic;

namespace Labb2_DbFirst_Template.Entities;

public partial class Store
{
    public int Id { get; set; }

    public string StoreName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int District { get; set; }

    public string City { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public virtual ICollection<BookOrder> BookOrders { get; set; } = new List<BookOrder>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

   
}

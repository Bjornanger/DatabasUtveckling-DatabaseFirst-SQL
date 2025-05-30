using System;
using System.Collections.Generic;

namespace Labb2_DbFirst_Template.Entities;

public partial class Book
{
    public string Isbn13 { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Language { get; set; }

    public double? Price { get; set; }

    public DateOnly ReleaseDate { get; set; }

    public int? PublisherId { get; set; }

    
    public virtual ICollection<BookOrder> BookOrders { get; set; } = new List<BookOrder>();

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual BookPublisher? Publisher { get; set; }

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
}

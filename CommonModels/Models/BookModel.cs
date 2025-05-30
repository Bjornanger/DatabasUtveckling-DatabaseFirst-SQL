using System.Collections;
using Labb2_DbFirst_Template.Entities;

namespace CommonModels.Models;

public class BookModel
{
    public string Isbn13 { get; set; } = null!;
 
    public string Title { get; set; } = null!;

    public string? Language { get; set; }

    public double? Price { get; set; }

    public DateOnly ReleaseDate { get; set; }

    public int? PublisherId { get; set; }

    public int Amount { get; set; } 


    //public virtual ICollection<BookOrder> BookOrders { get; set; } = new List<BookOrder>();

    //public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual BookPublisher? Publisher { get; set; }
    
    public List<AuthorModel> Authors { get; set; } = new();

    public List<InventoryModel> Inventory { get; set; } = new();


}
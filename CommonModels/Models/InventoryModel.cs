using Labb2_DbFirst_Template.Entities;

namespace CommonModels.Models;

public class InventoryModel
{
    public int StoresId { get; set; }

    public string Isbn13 { get; set; } = null!;
  
    public int Amount { get; set; }

    public virtual Book Isbn13id { get; set; } = null!;

    public virtual Store Stores { get; set; } = null!;
    public List<Inventory> Inventory { get; set; } = new();


}
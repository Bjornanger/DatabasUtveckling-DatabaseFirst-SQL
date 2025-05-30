using CommonModels.Models;
using Labb2_DbFirst_Template;
using Microsoft.EntityFrameworkCore;

namespace CommonModels.Services;

public class StoreRepository
{
    private readonly BookStoreForLabb2Context _context = new();

    private readonly BookRepository _bookRepository;
    private readonly InventoryRepository _inventoryRepository;

    public StoreRepository(BookRepository bookRepository, InventoryRepository inventoryRepository)
    {
        _bookRepository = bookRepository;
        _inventoryRepository = inventoryRepository;
    }

    public StoreRepository()
    {
        
    }

    public List<StoreModel> GetAllStores()
    {
        return _context.Stores.Select(
            store => new StoreModel
            {
                Id = store.Id,
                StoreName = store.StoreName,
                City = store.City,
                Address = store.Address
               
            }).ToList();

    }
    public StoreModel GetStoreById(int storeId)
    {
        var store = _context.Stores.Find(storeId);
        return new StoreModel
        {
            Id = store.Id,
            StoreName = store.StoreName,
            City = store.City,
            Address = store.Address,
           
        };
    }
    
  

}












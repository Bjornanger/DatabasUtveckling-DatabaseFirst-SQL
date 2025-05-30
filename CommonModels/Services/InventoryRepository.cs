using CommonModels.Models;
using Labb2_DbFirst_Template;
using Labb2_DbFirst_Template.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommonModels.Services;

public class InventoryRepository : BookModel
{
    private readonly BookStoreForLabb2Context _context = new();

    private readonly BookRepository? _bookRepository;
    private readonly StoreRepository? _storeRepository;
    public InventoryRepository(BookRepository bookRepository, StoreRepository storeRepository)
    {
        _bookRepository = bookRepository;
        _storeRepository = storeRepository;
    }

    public InventoryRepository()
    {

    }



    public List<InventoryModel> GetAllInventories()
    {
        return _context.Inventories.Select(invent => new InventoryModel
        {
            StoresId = invent.StoresId,
            Isbn13 = invent.Isbn13id,
            Amount = invent.Amount

        }).ToList();
    }
    

    public List<InventoryModel> GetInventoryByStoreId(int storeId)
    {
        var selectedInventories = _context.Inventories.Where(i => i.StoresId == storeId);

        return selectedInventories.Select(
            inventory => new InventoryModel
            {
                Isbn13 = inventory.Isbn13id,
                StoresId = inventory.StoresId,
                Stores = inventory.Stores,
                Amount = inventory.Amount,
            }
        ).ToList();
    }
    

}
using CommonModels.Models;
using Labb2_DbFirst_Template;
using Labb2_DbFirst_Template.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommonModels.Services;

public class AuthorRepository
{
    private readonly BookStoreForLabb2Context _context = new();

    private readonly InventoryRepository? _inventoryRepository;
    private readonly StoreRepository? _storeRepository;
    private readonly BookRepository _bookRepository;

    public AuthorRepository(InventoryRepository? inventoryRepository, StoreRepository? storeRepository, BookRepository bookRepository)
    {
        _inventoryRepository = inventoryRepository;
        _storeRepository = storeRepository;
        _bookRepository = bookRepository;
    }

    public AuthorRepository()
    {
        
    }
    public void AddNewAuthor(AuthorModel author)
    {
        var a = _context.Authors.Add(new Author
        {
            Id = author.Id,
            FirstName = author.FirstName,
            LastName = author.LastName,
            DateOfBirth = author.DateOfBirth,
            DateOfDeath = author.DateOfDeath,

        });
        _context.SaveChanges();
    }


    public List<AuthorModel> GetAllAuthors()
    {
        return _context.Authors.Select(
            author => new AuthorModel
        {
            Id = author.Id,
            FirstName = author.FirstName,
            LastName = author.LastName,
            DateOfBirth = author.DateOfBirth,
            DateOfDeath = author.DateOfDeath,
        }).ToList();
    }

    public void UpdateAuthorInfo(AuthorModel author)
    {

        var a = _context.Authors.Update(new Author
        {
            Id = author.Id,
            FirstName = author.FirstName,
            LastName = author.LastName,
            DateOfBirth = author.DateOfBirth,
            DateOfDeath = author.DateOfDeath,
        });
        _context.SaveChanges();
    }

    public void DeleteAuthor(int authorId)
    {
        var author = _context.Authors.Find(authorId);
        _context.Authors.Remove(author);
        _context.SaveChanges();

    }

    

}
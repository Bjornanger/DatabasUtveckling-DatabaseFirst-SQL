using CommonModels.Models;
using Labb2_DbFirst_Template;
using Labb2_DbFirst_Template.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommonModels.Services;

public class BookRepository
{

    private readonly BookStoreForLabb2Context _context = new();

    private readonly InventoryRepository? _inventoryRepository;
    private readonly StoreRepository? _storeRepository;

    public BookRepository()
    {

    }

    public BookRepository(InventoryRepository inventoryRepository, StoreRepository storeRepository)
    {
        _inventoryRepository = inventoryRepository;
        _storeRepository = storeRepository;
    }

    public BookModel AddNewBook(BookModel newBook, int authorId)
    {

        var authorToShow = _context.Authors.Include(b => b.BookIsbn13s).Where(a => a.Id == authorId);

        if (authorToShow == null)
        {
            return null;
        }

        var b = _context.Books.Add(
            new Book
            {
                Isbn13 = newBook.Isbn13,
                Title = newBook.Title,
                Price = newBook.Price,
                Language = newBook.Language,
                Publisher = newBook.Publisher,
                ReleaseDate = newBook.ReleaseDate,
                Authors = authorToShow.Select(a => new Author
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    DateOfBirth = a.DateOfBirth,
                    DateOfDeath = a.DateOfDeath
                }).ToList(),
            });

        _context.SaveChanges();
        return newBook;
    }



    public BookModel GetBookByIsbn(string id)
    {
        var book = _context.Books.FirstOrDefault(b => b.Isbn13 == id);

        if (book is null)
        {
            return null;
        }

        return new BookModel
        {
            Isbn13 = book.Isbn13,
            Title = book.Title,
            Language = book.Language,
            Price = book.Price,
            Publisher = book.Publisher,
            ReleaseDate = book.ReleaseDate,
        };
    }


    public List<BookModel> GetAllBooks()
    {
        return _context.Books.Select(
            book => new BookModel
            {
                Isbn13 = book.Isbn13,
                Title = book.Title,
                Language = book.Language,
                Price = book.Price,
                Publisher = book.Publisher,
                ReleaseDate = book.ReleaseDate,
                Amount = book.Inventories.Sum(i => i.Amount)
            }).ToList();
    }

    public void UpdateInventoryByStoreId(BookModel selectedBookToChange, int storeId)
    {
        var selectedInventory = _context.Inventories.FirstOrDefault(i => i.StoresId == storeId && i.Isbn13id == selectedBookToChange.Isbn13);

        if (selectedInventory == null)
        {
            return;
        }
        selectedInventory.Amount = selectedBookToChange.Amount;

        _context.SaveChanges();

    }
    


    public void UpdateBook(BookModel book)
    {
        var b = _context.Books.Update(
                new Book
                {
                    Isbn13 = book.Isbn13,
                    Title = book.Title,
                    Price = book.Price,
                    Language = book.Language,
                    ReleaseDate = book.ReleaseDate,
                    Publisher = book.Publisher,
                    Authors = book.Authors.Select(a => new Author
                    {
                        Id = a.Id,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        DateOfBirth = a.DateOfBirth,
                        DateOfDeath = a.DateOfDeath
                    }).ToList(),


                });
        _context.SaveChanges();
    }


    public void DeleteBook(string bookId)
    {
        var b = _context.Books.Find(bookId);
        _context.Books.Remove(b);
        _context.SaveChanges();
    }


    public List<BookModel>? GetBookByAuthor(int authorId)
    {
        
        var authorToShow = _context.Authors.Include(b => b.BookIsbn13s).FirstOrDefault(a => a.Id == authorId);

        if (authorToShow == null)
        {
            return null;
        }


        var authorBookList = new List<BookModel>();

        foreach (var book in authorToShow.BookIsbn13s)
        {

            var bookmodel = new BookModel()
            {
                Isbn13 = book.Isbn13,
                Title = book.Title,
                Language = book.Language,
                Price = book.Price,
                ReleaseDate = book.ReleaseDate
            };

            authorBookList.Add(bookmodel);
        }
        return authorBookList;
    }




}
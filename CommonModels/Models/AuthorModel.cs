using Labb2_DbFirst_Template.Entities;

namespace CommonModels.Models;

public class AuthorModel
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public DateOnly? DateOfDeath { get; set; }

    
    public List<Book> BooksInventory { get; set; } = new();

}
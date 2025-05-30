using System;
using System.Collections.Generic;

namespace Labb2_DbFirst_Template.Entities;

public partial class Author
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public DateOnly? DateOfDeath { get; set; }

    public virtual ICollection<Book> BookIsbn13s { get; set; } = new List<Book>();
}

using System;
using System.Collections.Generic;

namespace DataAccess.Entities.ViewsFromDB;

public partial class VDoubleAuthor
{
    public string Author { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Isbn13 { get; set; } = null!;

    public DateOnly ReleaseDate { get; set; }
}

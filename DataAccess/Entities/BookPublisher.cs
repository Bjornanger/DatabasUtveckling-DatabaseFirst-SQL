﻿using System;
using System.Collections.Generic;

namespace Labb2_DbFirst_Template.Entities;

public partial class BookPublisher
{
    public int Id { get; set; }

    public string PublisherName { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}

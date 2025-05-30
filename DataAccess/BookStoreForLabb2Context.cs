using DataAccess.Entities.ViewsFromDB;
using Labb2_DbFirst_Template.Entities;
using Microsoft.EntityFrameworkCore;

namespace Labb2_DbFirst_Template;

public partial class BookStoreForLabb2Context : DbContext
{
    public BookStoreForLabb2Context()
    {
    }

    public BookStoreForLabb2Context(DbContextOptions<BookStoreForLabb2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookOrder> BookOrders { get; set; }

    public virtual DbSet<BookPublisher> BookPublishers { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<VDoubleAuthor> VDoubleAuthors { get; set; }

    public virtual DbSet<VOrderReciept> VOrderReciepts { get; set; }

    public virtual DbSet<VStoreAndBook> VStoreAndBooks { get; set; }

    public virtual DbSet<VTitlesPerAuthor> VTitlesPerAuthors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Author__3214EC07F1AD9513");

            entity.ToTable("Author");

            entity.HasMany(d => d.BookIsbn13s).WithMany(p => p.Authors)
                .UsingEntity<Dictionary<string, object>>(
                    "AuthorDisplay",
                    r => r.HasOne<Book>().WithMany()
                        .HasForeignKey("BookIsbn13")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AuthorDisplay_Books"),
                    l => l.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AuthorDisplay_Author"),
                    j =>
                    {
                        j.HasKey("AuthorId", "BookIsbn13");
                        j.ToTable("AuthorDisplay");
                        j.IndexerProperty<string>("BookIsbn13")
                            .HasMaxLength(13)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("BookISBN13");
                    });
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Isbn13).HasName("PK__Books__3BF79E0305AC65C0");

            entity.Property(e => e.Isbn13)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ISBN13");
            entity.Property(e => e.Language).HasDefaultValue("English");
            entity.Property(e => e.Price).HasDefaultValueSql("((0.00))");
            entity.Property(e => e.ReleaseDate).HasColumnName("Release_Date");

            entity.HasOne(d => d.Publisher).WithMany(p => p.Books)
                .HasForeignKey(d => d.PublisherId)
                .HasConstraintName("FK__Books__Publisher__2B3F6F97");
        });

        modelBuilder.Entity<BookOrder>(entity =>
        {
            entity.HasKey(e => e.OrderNumberId).HasName("PK__Book_Ord__E6D543C968B3AB2C");

            entity.ToTable("Book_Order");

            entity.Property(e => e.BookIsbnid)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("BookISBNId");
            entity.Property(e => e.Quantity).HasDefaultValue(0);

            entity.HasOne(d => d.BookIsbn).WithMany(p => p.BookOrders)
                .HasForeignKey(d => d.BookIsbnid)
                .HasConstraintName("FK__Book_Orde__BookI__3B75D760");

            entity.HasOne(d => d.Customer).WithMany(p => p.BookOrders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Book_Orde__Custo__3A81B327");

            entity.HasOne(d => d.Employee).WithMany(p => p.BookOrders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Book_Orde__Emplo__398D8EEE");

            entity.HasOne(d => d.Store).WithMany(p => p.BookOrders)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK__Book_Orde__Store__38996AB5");
        });

        modelBuilder.Entity<BookPublisher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Book_Pub__3214EC0701C00D30");

            entity.ToTable("Book_Publisher");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.PublisherName).HasColumnName("Publisher_Name");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC07400C94A3");

            entity.ToTable("Customer");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC071E8FEC58");

            entity.ToTable("Employee");

            entity.HasOne(d => d.Store).WithMany(p => p.Employees)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK__Employee__StoreI__35BCFE0A");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => new { e.StoresId, e.Isbn13id });

            entity.ToTable("Inventory");

            entity.Property(e => e.Isbn13id)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ISBN13Id");

            entity.HasOne(d => d.Isbn13).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.Isbn13id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookISBN_Books");

            entity.HasOne(d => d.Stores).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.StoresId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StoresId_Stores");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Stores__3214EC079DF8255D");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Phone_Number");
            entity.Property(e => e.StoreName).HasColumnName("Store_Name");
        });

        modelBuilder.Entity<VDoubleAuthor>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vDoubleAuthor");

            entity.Property(e => e.Isbn13)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ISBN13");
            entity.Property(e => e.ReleaseDate).HasColumnName("Release Date");
        });

        modelBuilder.Entity<VOrderReciept>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vOrder_Reciept");

            entity.Property(e => e.CustomerInfo).HasColumnName("Customer Info");
            entity.Property(e => e.IdClerkName).HasColumnName(" ID Clerk Name");
            entity.Property(e => e.IdStore).HasColumnName("ID Store");
            entity.Property(e => e.OrderDate).HasMaxLength(4000);
            entity.Property(e => e.OrderId)
                .HasMaxLength(10)
                .HasColumnName("Order ID");
            entity.Property(e => e.Price)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.TotalSum)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("Total Sum");
        });

        modelBuilder.Entity<VStoreAndBook>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vStoreAndBooks");

            entity.Property(e => e.Isbn)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ISBN");
            entity.Property(e => e.StoreId).HasColumnName("Store ID");
            entity.Property(e => e.StoreName).HasColumnName("Store Name");
        });

        modelBuilder.Entity<VTitlesPerAuthor>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vTitlesPerAuthor");

            entity.Property(e => e.Age)
                .HasMaxLength(18)
                .IsUnicode(false);
            entity.Property(e => e.BooksSt).HasColumnName("Books (st)");
            entity.Property(e => e.StockValue)
                .HasMaxLength(58)
                .IsUnicode(false)
                .HasColumnName("Stock Value");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Proposal1
{
    public partial class TBMContext : DbContext
    {
        public TBMContext()
        {
        }

        public TBMContext(DbContextOptions<TBMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookReturn> BookReturns { get; set; }
        public virtual DbSet<BookSale> BookSales { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Isbnauthor> Isbnauthors { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<StockBalance> StockBalances { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<TitlesPerAuthor> TitlesPerAuthors { get; set; }
        public virtual DbSet<TotalBookSale> TotalBookSales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
                optionsBuilder.UseSqlServer("Server=DESKTOP-EMRMARH;Database=TBM;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("Author");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Isbn13)
                    .HasName("PK__Books");

                entity.Property(e => e.Isbn13)
                    .ValueGeneratedNever()
                    .HasColumnName("ISBN13");

                entity.Property(e => e.Language)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Publisher)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReleaseDate)
                    .HasColumnType("date")
                    .HasColumnName("Release Date");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BookReturn>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.AmountRefunded).HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Isbn).HasColumnName("ISBN");

                entity.Property(e => e.OrderId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OrderID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.HasOne(d => d.IsbnNavigation)
                    .WithMany(p => p.BookReturns)
                    .HasForeignKey(d => d.Isbn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookReturns_Books");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.BookReturns)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookReturns_Orders");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.BookReturns)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookReturns_Stores");
            });

            modelBuilder.Entity<BookSale>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Isbn).HasColumnName("ISBN");

                entity.Property(e => e.OrderId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OrderID");

                entity.Property(e => e.SalePrice).HasColumnType("decimal(7, 2)");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.HasOne(d => d.IsbnNavigation)
                    .WithMany(p => p.BookSales)
                    .HasForeignKey(d => d.Isbn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookSales_Books");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.BookSales)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookSales_Orders");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.BookSales)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_BookSales_Stores");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Isbnauthor>(entity =>
            {
                entity.HasKey(e => new { e.Isbn, e.AuthorId })
                    .HasName("PK_Lab2ISBNAuthors_1");

                entity.ToTable("ISBNAuthors");

                entity.Property(e => e.Isbn).HasColumnName("ISBN");

                entity.Property(e => e.AuthorId).HasColumnName("AuthorID");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Isbnauthors)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ISBNAuthors_Author");

                entity.HasOne(d => d.IsbnNavigation)
                    .WithMany(p => p.Isbnauthors)
                    .HasForeignKey(d => d.Isbn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ISBNAuthors_Books");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.BoutiqueId).HasColumnName("BoutiqueID");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.ShipDate).HasColumnType("date");

                entity.HasOne(d => d.Boutique)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.BoutiqueId)
                    .HasConstraintName("FK__Lab2Order__Stores");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Lab2Orders_Customers");
            });

            modelBuilder.Entity<StockBalance>(entity =>
            {
                entity.HasKey(e => new { e.BoutiqueId, e.Isbn })
                    .HasName("PK__StockBalance");

                entity.ToTable("StockBalance");

                entity.Property(e => e.BoutiqueId).HasColumnName("BoutiqueID");

                entity.Property(e => e.Isbn).HasColumnName("ISBN");

                entity.Property(e => e.Number).HasColumnName("NUMBER");

                entity.HasOne(d => d.Boutique)
                    .WithMany(p => p.StockBalances)
                    .HasForeignKey(d => d.BoutiqueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StockBalance_Stores");

                entity.HasOne(d => d.IsbnNavigation)
                    .WithMany(p => p.StockBalances)
                    .HasForeignKey(d => d.Isbn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StockBalance_Books");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.StoreAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StoreName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StorePhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TitlesPerAuthor>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("TitlesPerAuthor");

                entity.Property(e => e.Name)
                    .HasMaxLength(101)
                    .IsUnicode(false);

                entity.Property(e => e.StockValue)
                    .HasColumnType("decimal(38, 2)")
                    .HasColumnName("Stock Value");
            });

            modelBuilder.Entity<TotalBookSale>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Total Book Sales");

                entity.Property(e => e.GrossBookSales)
                    .HasColumnType("decimal(38, 2)")
                    .HasColumnName("Gross Book Sales");

                entity.Property(e => e.Isbn).HasColumnName("ISBN");

                entity.Property(e => e.NetSales)
                    .HasColumnType("decimal(38, 2)")
                    .HasColumnName("Net Sales");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

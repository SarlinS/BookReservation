using Microsoft.EntityFrameworkCore;

namespace BookReservation.Models
{
    // DbContext class responsible for interacting with the database
    public class BookContext : DbContext
    {
        // Constructor to initialize the DbContext with specified options
        public BookContext(DbContextOptions<BookContext> options)
            : base(options)
        {
        }

        // DbSet for the BookItem entity, representing books in the database
        public DbSet<BookItem> BookItems { get; set; } = null!;

        // DbSet for the BookStatusHistory entity, representing status change history of books
        public DbSet<BookStatusHistory> BookStatusHistories { get; set; } = null!;

        // Method to configure the relationships and constraints between entities
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define a one-to-many relationship between BookItem and BookStatusHistory
            modelBuilder.Entity<BookItem>()
                .HasMany(bookItem => bookItem.StatusHistory)
                .WithOne(statusHistory => statusHistory.BookItem)
                .HasForeignKey(statusHistory => statusHistory.BookItemId);
        }
    }
}

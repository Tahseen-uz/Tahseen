using Microsoft.EntityFrameworkCore;
using Tahseen.Domain.Entities;
using Tahseen.Domain.Entities.Books;

namespace Tahseen.Data.DbContexts;

public class AppDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<BookReviews> BookReviews { get; set; }
    public DbSet<CompletedBooks> CompletedBooks { get; set; }
    public DbSet<Publisher> Publisher { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=ProductOrderDb;Username=postgres;Password=0808080808");
    }
}

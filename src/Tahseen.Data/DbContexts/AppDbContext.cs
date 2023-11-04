using Tahseen.Domain.Entities;
using Tahseen.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Tahseen.Domain.Entities.Books;
using Tahseen.Domain.Entities.Events;
using Tahseen.Domain.Entities.Library;
using Tahseen.Domain.Entities.Rewards;
using Tahseen.Domain.Entities.Librarians;
using Tahseen.Domain.Entities.Reservations;
using Tahseen.Domain.Entities.Notifications;
using Tahseen.Domain.Entities.SchoolAndEducations;
using Tahseen.Domain.Entities.Feedbacks;

namespace Tahseen.Data.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
    
    //Folder Name: Books
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookReviews> BookReviews { get; set; }
    public DbSet<CompletedBooks> CompletedBooks { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Publisher> Publishers { get; set; }

    //Folder Name: Events
    public DbSet<Event> Events { get; set; }
    public DbSet<EventRegistration> EventsRegistration { get; set; }

    //Folder Name: Feedbacks
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<News> News { get; set; }
    public DbSet<SurveyResponses> SurveyResponses { get; set; }
    public DbSet<Surveys> Surveys { get; set; }
    public DbSet<UserMessage> UserMessages { get; set; }
    public DbSet<UserRatings> UserRatings { get; set; }

    //Folder Name: Librarians  
    public DbSet<Librarian> Librarians { get; set; }

    //Folder Name: Libraries
    public DbSet<LibraryBranch> LibraryBranches { get; set; }
    public DbSet<LibraryStatistics> LibraryStatistics { get; set; }

    //Notification
    public DbSet<Notification> Notification { get; set; }

    //Folder Name: Reservation
    public DbSet<Reservation> Reservations { get; set; }

    //Folder Name: Rewards
    public DbSet<Badge> Badges { get; set; }
    public DbSet<UserBadges> UserBadges { get; set; }

    //Folder Name: SchoolAndEducation
    public DbSet<Pupil> Pupils { get; set; }
    public DbSet<PupilBookConnection> PupilBookConnections { get; set; }
    public DbSet<SchoolBook> SchoolBooks { get; set; }

    //Folder Name: UserAuthentication
    public DbSet<Authentication> Authentications { get; set; }

    //Folder Name: Users
    public DbSet<BorrowedBook> BorrowedBooks { get; set; }
    public DbSet<BorrowedBookCart> BorrowedBookCarts { get; set; }
    public DbSet<Fine> Fines { get; set; }
    public DbSet<Registration> Registrations { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserCart> UserCarts { get; set; }
    public DbSet<UserProgressTracking> UserProgressTracking { get; set; }
    public DbSet<UserSettings> UserSettings { get; set; }
    public DbSet<WishList> WishLists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId);

        modelBuilder.Entity<Book>()
            .HasOne(b => b.Publisher)
            .WithMany(p => p.Books)
            .HasForeignKey(b => b.PublisherId);
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Tahseen.Domain.Entities.Books;
using Tahseen.Domain.Entities.Languages;
using Tahseen.Domain.Entities.Library;
using Tahseen.Domain.Enums;
using Tahseen.Service.DTOs.Books.Author;
using Tahseen.Service.DTOs.Books.BookReviews;
using Tahseen.Service.DTOs.Books.CompletedBooks;
using Tahseen.Service.DTOs.Books.Genre;
using Tahseen.Service.DTOs.Books.Publishers;
using Tahseen.Service.DTOs.Librarians;
using Tahseen.Service.DTOs.Libraries.LibraryBranch;
using Tahseen.Service.DTOs.Users.BorrowedBook;

namespace Tahseen.Service.DTOs.Books.Book;

public class BookForResultDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public long TotalCopies { get; set; }
    public long AvailableCopies { get; set; }
    public decimal Rating { get; set; }
    public long Reviews { get; set; }
    public string Content { get; set; }
    public string BookFormat { get; set; }
    public string ShelfLocation { get; set; }
    public string Condition { get; set; }
    public string BookImage { get; set; }
    public AuthorForResultDto Author { get; set; }
    public GenreForResultDto Genre { get; set; }
    public LibraryBranchForResultDto LibraryBranch { get; set; }
    public PublisherForResultDto Publisher { get; set; }
    public BookReviewForResultDto BookReviews { get; set; }
    public Language Language { get; set; }
    public IEnumerable<BorrowedBookForResultDto> Borrowers { get; set; }
    public string PrintedIn { get; set; }
    public long TotalPages { get; set; }


    /* public IEnumerable<AuthorForResultDto> BookAuthor { get; set; }
     public IEnumerable<GenreForResultDto> BookGenre { get; set; }
     public IEnumerable<PublisherForResultDto> BookPublisher { get; set; }*/
}
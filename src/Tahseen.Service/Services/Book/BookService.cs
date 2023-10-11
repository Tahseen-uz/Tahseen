using Tahseen.Data.IRepositories;

namespace Tahseen.Service.Services.Book;

public class BookService:BaseService<Domain.Entities.Books.Book>
{
    public BookService(IRepostory<Domain.Entities.Books.Book> repostory) : base(repostory)
    {
    }
}
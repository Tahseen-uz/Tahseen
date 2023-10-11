using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.Books;

namespace Tahseen.Service.Services.Book;

public class AutherService:BaseService<Author>
{
    public AutherService(IRepostory<Author> repostory) : base(repostory)
    {
    }
}
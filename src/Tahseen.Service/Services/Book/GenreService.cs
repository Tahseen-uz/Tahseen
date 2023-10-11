using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.Books;

namespace Tahseen.Service.Services.Book;

public class GenreService:BaseService<Genre>
{
    public GenreService(IRepostory<Genre> repostory) : base(repostory)
    {
    }
}
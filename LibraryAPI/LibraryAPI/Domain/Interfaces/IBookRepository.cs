using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Interfaces;

public interface IBookRepository : IBaseRepository<Book>
{
    Task<IEnumerable<Book>> GetBooksWithDetailsAsync();
}
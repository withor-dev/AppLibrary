using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Interfaces;
using LibraryAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Infrastructure.Repositories;

public class BookRepository : BaseRepository<Book>, IBookRepository
{
    public BookRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Book>> GetBooksWithDetailsAsync()
    {
        return await _dbSet
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .ToListAsync();
    }
}
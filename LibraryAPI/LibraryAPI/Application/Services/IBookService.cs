using LibraryAPI.Application.DTOs;
using LibraryAPI.Application.ViewModels;

namespace LibraryAPI.Application.Services;

public interface IBookService
{
    Task<IEnumerable<BookViewModel>> GetAllAsync();
    Task<BookViewModel?> GetByIdAsync(Guid id);
    Task<BookViewModel> AddAsync(BookCreateDTO dto);
    Task UpdateAsync(BookUpdateDTO dto);
    Task DeleteAsync(Guid id);
}
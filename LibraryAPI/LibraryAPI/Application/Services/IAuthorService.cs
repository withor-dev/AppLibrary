using LibraryAPI.Application.DTOs;
using LibraryAPI.Application.ViewModels;

namespace LibraryAPI.Application.Services;

public interface IAuthorService
{
    Task<IEnumerable<AuthorViewModel>> GetAllAsync();
    Task<AuthorViewModel?> GetByIdAsync(Guid id);
    Task<AuthorViewModel> AddAsync(AuthorCreateDTO dto);
    Task UpdateAsync(AuthorUpdateDTO dto);
    Task DeleteAsync(Guid id);
}
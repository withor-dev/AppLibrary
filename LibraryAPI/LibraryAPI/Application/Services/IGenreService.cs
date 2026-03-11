using LibraryAPI.Application.DTOs;
using LibraryAPI.Application.ViewModels;

namespace LibraryAPI.Application.Services;

public interface IGenreService
{
    Task<IEnumerable<GenreViewModel>> GetAllAsync();
    Task<GenreViewModel?> GetByIdAsync(Guid id);
    Task<GenreViewModel> AddAsync(GenreCreateDTO dto);
    Task UpdateAsync(GenreUpdateDTO dto);
    Task DeleteAsync(Guid id);
}
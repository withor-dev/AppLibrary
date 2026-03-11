using LibraryAPI.Application.DTOs;
using LibraryAPI.Application.ViewModels;
using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Interfaces;

namespace LibraryAPI.Application.Services;

public class GenreService : IGenreService
{
    private readonly IBaseRepository<Genre> _genreRepository;

    public GenreService(IBaseRepository<Genre> genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<IEnumerable<GenreViewModel>> GetAllAsync()
    {
        var genres = await _genreRepository.GetAllAsync();
        return genres.Select(g => new GenreViewModel { Id = g.Id, Name = g.Name });
    }

    public async Task<GenreViewModel?> GetByIdAsync(Guid id)
    {
        var genre = await _genreRepository.GetByIdAsync(id);
        if (genre == null) return null;

        return new GenreViewModel { Id = genre.Id, Name = genre.Name };
    }

    public async Task<GenreViewModel> AddAsync(GenreCreateDTO dto)
    {
        var genre = new Genre(dto.Name);

        await _genreRepository.AddAsync(genre);
        await _genreRepository.SaveChangesAsync();

        return new GenreViewModel { Id = genre.Id, Name = genre.Name };
    }

    public async Task UpdateAsync(GenreUpdateDTO dto)
    {
        var genre = await _genreRepository.GetByIdAsync(dto.Id);
        if (genre == null) throw new ArgumentException("Gênero não encontrado.");

        genre.UpdateName(dto.Name);

        _genreRepository.Update(genre);
        await _genreRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var genre = await _genreRepository.GetByIdAsync(id);
        if (genre == null) throw new ArgumentException("Gênero não encontrado.");

        _genreRepository.Delete(genre);
        await _genreRepository.SaveChangesAsync();
    }
}
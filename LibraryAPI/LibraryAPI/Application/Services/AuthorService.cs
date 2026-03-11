using LibraryAPI.Application.DTOs;
using LibraryAPI.Application.ViewModels;
using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Interfaces;

namespace LibraryAPI.Application.Services;

public class AuthorService : IAuthorService
{
    private readonly IBaseRepository<Author> _authorRepository;

    public AuthorService(IBaseRepository<Author> authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<IEnumerable<AuthorViewModel>> GetAllAsync()
    {
        var authors = await _authorRepository.GetAllAsync();
        return authors.Select(a => new AuthorViewModel { Id = a.Id, Name = a.Name });
    }

    public async Task<AuthorViewModel?> GetByIdAsync(Guid id)
    {
        var author = await _authorRepository.GetByIdAsync(id);
        if (author == null) return null;

        return new AuthorViewModel { Id = author.Id, Name = author.Name };
    }

    public async Task<AuthorViewModel> AddAsync(AuthorCreateDTO dto)
    {
        var author = new Author(dto.Name);

        await _authorRepository.AddAsync(author);
        await _authorRepository.SaveChangesAsync();

        return new AuthorViewModel { Id = author.Id, Name = author.Name };
    }

    public async Task UpdateAsync(AuthorUpdateDTO dto)
    {
        var author = await _authorRepository.GetByIdAsync(dto.Id);
        if (author == null) throw new ArgumentException("Autor não encontrado.");

        author.UpdateName(dto.Name);

        _authorRepository.Update(author);
        await _authorRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var author = await _authorRepository.GetByIdAsync(id);
        if (author == null) throw new ArgumentException("Autor não encontrado.");

        _authorRepository.Delete(author);
        await _authorRepository.SaveChangesAsync();
    }
}
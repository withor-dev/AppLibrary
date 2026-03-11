namespace LibraryAPI.Application.DTOs;

public record BookCreateDTO(string Title, Guid AuthorId, Guid GenreId);
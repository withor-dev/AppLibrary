namespace LibraryAPI.Application.DTOs;

public record BookUpdateDTO(Guid Id, string Title, Guid AuthorId, Guid GenreId);
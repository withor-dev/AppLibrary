using Asp.Versioning;
using LibraryAPI.Application.DTOs;
using LibraryAPI.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class GenresController : ControllerBase
{
    private readonly IGenreService _genreService;

    public GenresController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var genres = await _genreService.GetAllAsync();
        return Ok(genres);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var genre = await _genreService.GetByIdAsync(id);
        if (genre == null)
            return NotFound(new { message = "Gênero não encontrado." });

        return Ok(genre);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] GenreCreateDTO dto)
    {
        try
        {
            var genre = await _genreService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = genre.Id, version = "1.0" }, genre);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] GenreUpdateDTO dto)
    {
        if (id != dto.Id)
            return BadRequest(new { message = "O ID da rota difere do ID do corpo da requisição." });

        try
        {
            await _genreService.UpdateAsync(dto);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _genreService.DeleteAsync(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500,
                new { message = "Não é possível excluir este gênero pois existem livros vinculados a ele." });
        }
    }
}
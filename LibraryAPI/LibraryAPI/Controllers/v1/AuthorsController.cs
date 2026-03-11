using Asp.Versioning;
using LibraryAPI.Application.DTOs;
using LibraryAPI.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var authors = await _authorService.GetAllAsync();
        return Ok(authors);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var author = await _authorService.GetByIdAsync(id);
        if (author == null)
            return NotFound(new { message = "Autor não encontrado." });

        return Ok(author);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AuthorCreateDTO dto)
    {
        try
        {
            var author = await _authorService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = author.Id, version = "1.0" }, author);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] AuthorUpdateDTO dto)
    {
        if (id != dto.Id)
            return BadRequest(new { message = "O ID da rota difere do ID do corpo da requisição." });

        try
        {
            await _authorService.UpdateAsync(dto);
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
            await _authorService.DeleteAsync(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500,
                new { message = "Não é possível excluir este autor pois existem livros vinculados a ele." });
        }
    }
}
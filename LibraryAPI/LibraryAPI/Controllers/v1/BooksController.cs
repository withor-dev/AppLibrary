using Asp.Versioning;
using LibraryAPI.Application.DTOs;
using LibraryAPI.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var books = await _bookService.GetAllAsync();
        return Ok(books);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var book = await _bookService.GetByIdAsync(id);
        if (book == null)
            return NotFound(new { message = "Livro não encontrado." });

        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BookCreateDTO dto)
    {
        try
        {
            var book = await _bookService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = book.Id, version = "1.0" }, book);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] BookUpdateDTO dto)
    {
        if (id != dto.Id)
            return BadRequest(new { message = "O ID da rota difere do ID do corpo da requisição." });

        try
        {
            await _bookService.UpdateAsync(dto);
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
            await _bookService.DeleteAsync(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "Erro ao excluir. O registro pode estar em uso." });
        }
    }
}
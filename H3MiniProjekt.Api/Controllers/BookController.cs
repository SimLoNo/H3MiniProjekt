using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using H3MiniProjekt.DAL.Database;
using H3MiniProjekt.DAL.Database.Models;
using H3MiniProjekt.DAL.Repositories;

namespace H3MiniProjekt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _repository;

        public BookController(IBookRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Book
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            try
            {
                List<Book> books = await _repository.GetAllBooks();
                if (books == null)
                {
                    return Problem("List of books was null, this was unexpected.");
                }
                if (books.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(books);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GET: api/Book/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            try
            {
                var book = await _repository.GetBookById(id);

                if (book == null)
                {
                    return NotFound();
                }
                return Ok(book);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // PUT: api/Book/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.BookId)
            {
                return BadRequest();
            }

            try
            {
                Book returnedBook = await _repository.UpdateBook(id, book);

                if (returnedBook == null)
                {
                    return NotFound();
                }
                return Ok(returnedBook);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // POST: api/Book
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book) // Spoerg Flemming omkring Post/create.
        {
            try
            {
                Book postedBook = await _repository.CreateBook(book);

                return CreatedAtAction("GetBook", new { id = book.BookId }, book);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // DELETE: api/Book/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                Book deletedBook = await _repository.DeleteBook(id);
                if (deletedBook == null)
                {
                    return NotFound();
                }
                return Ok(deletedBook);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}

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
    public class AuthorController : ControllerBase
    {
        //private readonly AbContext _context;

        //public AuthorController(AbContext context)
        //{
        //    _context = context;
        //}

        private readonly IAuthorRepository _repository;

        public AuthorController(IAuthorRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Author
        [HttpGet]
        public async Task<ActionResult> GetAllAuthors()
        {
            try
            {
                List<Author> authors = await _repository.GetAllAuthors();
                if (authors == null)
                {
                    return Problem("List of authors replied from the server was null, this was unexpected.");
                }
                if (authors.Count == 0)
                {
                    return NoContent();
                }
                return Ok(authors);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GET: api/Author/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAuthor(int id)
        {
            try
            {
                Author author = await _repository.GetAuthorById(id);

                if (author == null)
                {
                    return NotFound();
                }
                return Ok(author);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // PUT: api/Author/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
            try
            {
                if (id != author.AuthorId)
                {
                    return BadRequest();
                }
                Author updatedAuthor = await _repository.UpdateAuthor(id, author);
                if (updatedAuthor == null)
                {
                    return NotFound();
                }
                return Ok(updatedAuthor);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // POST: api/Author
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostAuthor(Author author)
        {
            try
            {
                Author createdAuthor = await _repository.CreateAuthor(author);
                if (createdAuthor == null)
                {
                    return NotFound();
                }
                return Ok(createdAuthor);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // DELETE: api/Author/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                Author deletedAuthor = await _repository.DeleteAuthor(id);
                if (deletedAuthor == null)
                {
                    return NotFound();
                }
                return Ok(deletedAuthor);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}

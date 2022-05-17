using H3MiniProjekt.DAL.Database;
using H3MiniProjekt.DAL.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3MiniProjekt.DAL.Repositories
{
    public interface IAuthorRepository
    {
        public Task<List<Author>> GetAllAuthors();
        public Task<Author> GetAuthorById(int AuthorId);
        Task<Author> CreateAuthor(Author author);
        Task<Author> DeleteAuthor(int authorId);
        Task<Author> UpdateAuthor(int autherId,Author author);
    }
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AbContext _context;

        public AuthorRepository(AbContext context)
        {
            _context = context;
        }


        public async Task<Author> CreateAuthor(Author author)
        {
            _context.Author.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<Author> DeleteAuthor(int authorId)
        {
            Author author = await _context.Author.FirstOrDefaultAsync(authorObj => authorObj.AuthorId == authorId);
            if (author != null)
            {
                _context.Author.Remove(author);
                await _context.SaveChangesAsync();
            }
            return author;
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            return await _context.Author.ToListAsync();
        }

        public async Task<Author> GetAuthorById(int AuthorId)
        {
            return await _context.Author.FirstOrDefaultAsync(Author => Author.AuthorId == AuthorId);
        }

        public async Task<Author> UpdateAuthor(int authorId, Author author)
        {
            Author updateAuthor = await _context.Author.FirstOrDefaultAsync(authorObj => authorObj.AuthorId == authorId);

            if (updateAuthor != null)
            {
                updateAuthor.Name = author.Name;
                updateAuthor.Age = author.Age;
                updateAuthor.Password = author.Password;
                updateAuthor.IsAlive = author.IsAlive;
                await _context.SaveChangesAsync();

            }
            return updateAuthor;
        }
    }
}

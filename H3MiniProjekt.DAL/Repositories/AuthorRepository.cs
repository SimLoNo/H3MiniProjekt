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
        void CreateAuthor(Author author);
        void DeleteAuthor(int authorId);
        void UpdateAuthor(int autherId,Author author);
    }
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AbContext _context;

        public AuthorRepository(AbContext context)
        {
            _context = context;
        }


        public void CreateAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public void DeleteAuthor(int authorId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            return await _context.Author.ToListAsync();
        }

        public async Task<Author> GetAuthorById(int AuthorId)
        {
            return await _context.Author.FirstOrDefaultAsync(Author => Author.AuthorId == AuthorId);
        }

        public void UpdateAuthor(int autherId, Author author)
        {
            throw new NotImplementedException();
        }
    }
}

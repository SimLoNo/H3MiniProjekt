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
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBooks();
        Task<Book> GetBookById(int id);
        Task<Book> DeleteBook(int id);
        Task<Book> UpdateBook(int bookId,Book book);
        Task<Book> CreateBook(Book book);
    }
    public class BookRepository : IBookRepository
    {
        private readonly AbContext _context;

        public BookRepository(AbContext context)
        {
            _context = context;
        }
        public async Task<Book> CreateBook(Book book)
        {
            _context.Book.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> DeleteBook(int id)
        {
            Book book = await _context.Book.FirstOrDefaultAsync(bookObj => bookObj.BookId == id);
            if (book != null)
            {
                _context.Book.Remove(book);
                await _context.SaveChangesAsync();
            }
            return book;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _context.Book.ToListAsync();
        }

        public async Task<Book> GetBookById(int bookId)
        {
            return await _context.Book.FirstOrDefaultAsync(bookObj => bookObj.BookId == bookId);
        }

        public async Task<Book> UpdateBook(int bookId, Book book)
        {
            Book updateBook = await _context.Book.FirstOrDefaultAsync(bookObj => bookObj.BookId == bookId);

            if (updateBook != null)
            {
                updateBook.Title = book.Title;
                updateBook.Pages = book.Pages;
                updateBook.ReleaseYear = book.ReleaseYear;
                updateBook.Binding = book.Binding;
                updateBook.WordCound = book.WordCound;
                await _context.SaveChangesAsync();

            }
            return updateBook;
        }
    }
}

using H3MiniProjekt.DAL.Database;
using H3MiniProjekt.DAL.Database.Models;
using H3MiniProjekt.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace H3MiniProjekt.Tests.Repositories
{
    public class BookRepositoryTests
    {
        private readonly DbContextOptions<AbContext> _options;
        private readonly AbContext _context;
        private readonly BookRepository _bookRepository;

        public BookRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<AbContext>()
                .UseInMemoryDatabase(databaseName: "H3MiniProjektBook")
                .Options;

            _context = new(_options);

            _bookRepository = new(_context);
        }

        [Fact]
        public async void CreateBook_ShouldReturnBook()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();

            Book author = new()
            {
                BookId = 1,
                Title = "Jack",
                Pages = 20,
                WordCound = 20,
                Binding = false
            };
            //Act
            var result = await _bookRepository.CreateBook(author);
            //Assert
            Assert.IsType<Book>(result);

        }

        [Fact]
        public async void DeleteBook_ShouldReturnBook_WhenBookIsDeleted()
        {
            //Arrange
            int id = 1;
            Book author = new()
            {
                BookId = id,
                Title = "Jack",
                Pages = 20,
                WordCound = 20,
                Binding = false
            };
            await _context.Database.EnsureDeletedAsync();
            _context.Book.Add(author);
            await _context.SaveChangesAsync();

            //Act
            var result = await _bookRepository.DeleteBook(id);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<Book>(result);
        }

        [Fact]
        public async void DeleteBook_ShouldReturnNull_WhenNoBookIsFound()
        {
            //Arrange
            int id = 1;
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _bookRepository.DeleteBook(id);
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void GetAllBooks_ShouldReturnListOfBooks_WhenBooksExists()
        {
            //Arrange
            int id = 1;
            Book author1 = new()
            {

                BookId = id,
                Title = "Jack",
                Pages = 20,
                WordCound = 20,
                Binding = false
            };
            Book author2 = new()
            {

                BookId = id + 1,
                Title = "Jack",
                Pages = 20,
                WordCound = 20,
                Binding = false
            };
            await _context.Database.EnsureDeletedAsync();
            _context.Book.Add(author1);
            _context.Book.Add(author2);
            await _context.SaveChangesAsync();
            //Act
            var result = await _bookRepository.GetAllBooks();
            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<Book>>(result);
        }

        [Fact]
        public async void GetAllBooks_ShouldReturnEmptyListOfBooks_WhenNoBooksExists()
        {
            //Arrange

            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _bookRepository.GetAllBooks();
            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<Book>>(result);
        }

        [Fact]
        public async void GetBookById_ShouldReturnNull_WhenTheBookDoesNotExists()
        {
            //Arrange
            int id = 1;
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _bookRepository.GetBookById(id);
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void GetBookById_ShouldReturnBook_WhenTheBookExists()
        {
            //Arrange
            int id = 1;
            Book author = new()
            {
                BookId = id,
                Title = "Jack",
                Pages = 20,
                WordCound = 20,
                Binding = false
            };
            await _context.Database.EnsureDeletedAsync();
            _context.Book.Add(author);
            await _context.SaveChangesAsync();
            //Act
            var result = await _bookRepository.GetBookById(id);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<Book>(result);
            Assert.Equal(id, result.BookId);
        }

        [Fact]
        public async void UpdateBook_ShouldReturnNull_WhenNoBookIsUpdated()
        {
            //Arrange
            int id = 1;
            Book author = new()
            {
                BookId = id,
                Title = "Jack",
                Pages = 20,
                WordCound = 20,
                Binding = false
            };
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _bookRepository.UpdateBook(id, author);
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateBook_ShouldReturnUpdatedBook_WhenTheBookIsUpdated()
        {
            //Arrange
            int id = 1;
            Book author = new()
            {
                BookId = id,
                Title = "Jack",
                Pages = 20,
                WordCound = 20,
                Binding = false
            };
            Book newBook = new()
            {
                BookId = id,
                Title = "Jack",
                Pages = 20,
                WordCound = 20,
                Binding = false
            };
            await _context.Database.EnsureDeletedAsync();
            _context.Book.Add(author);
            await _context.SaveChangesAsync();
            //Act
            var result = await _bookRepository.UpdateBook(id, newBook);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<Book>(result);
            Assert.Equal(newBook.Title, result.Title);
            Assert.Equal(newBook.Pages, result.Pages);
            Assert.Equal(newBook.WordCound, result.WordCound);
            Assert.Equal(newBook.Binding, result.Binding);
        }
    }
}

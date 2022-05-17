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
    public class AuthorRepositoryTests
    {
        private readonly DbContextOptions<AbContext> _options;
        private readonly AbContext _context;
        private readonly AuthorRepository _authorRepository;

        public AuthorRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<AbContext>()
                .UseInMemoryDatabase(databaseName: "H3MiniProjektAuthor")
                .Options;

            _context = new(_options);

            _authorRepository = new(_context);
        }

        [Fact]
        public async void CreateAuthor_ShouldReturnAuthor()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();

            Author author = new()
            {
                AuthorId = 1,
                Name = "Jack",
                Age = 20,
                Password = "Aliena",
                IsAlive = false
            };
            //Act
            var result = await _authorRepository.CreateAuthor(author);
            //Assert
            Assert.IsType<Author>(result);

        }

        [Fact]
        public async void DeleteAuthor_ShouldReturnAuthor_WhenAuthorIsDeleted()
        {
            //Arrange
            int id = 1;
            Author author = new()
            {
                AuthorId = id,
                Name = "Jack",
                Age = 20,
                Password = "Aliena",
                IsAlive = false
            };
            await _context.Database.EnsureDeletedAsync();
            _context.Author.Add(author);
            await _context.SaveChangesAsync();
            
            //Act
            var result = await _authorRepository.DeleteAuthor(id);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<Author>(result);
        }

        [Fact]
        public async void DeleteAuthor_ShouldReturnNull_WhenNoAuthorIsFound()
        {
            //Arrange
            int id = 1;
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _authorRepository.DeleteAuthor(id);
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void GetAllAuthors_ShouldReturnListOfAuthors_WhenAuthorsExists()
        {
            //Arrange
            int id = 1;
            Author author1 = new()
            {

                AuthorId = id,
                Name = "Jack",
                Age = 20,
                Password = "Aliena",
                IsAlive = false
            };
            Author author2 = new()
            {

                AuthorId = id+1,
                Name = "Jack",
                Age = 20,
                Password = "Aliena",
                IsAlive = false
            };
            await _context.Database.EnsureDeletedAsync();
            _context.Author.Add(author1);
            _context.Author.Add(author2);
            await _context.SaveChangesAsync();
            //Act
            var result = await _authorRepository.GetAllAuthors();
            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<Author>>(result);
        }

        [Fact]
        public async void GetAllAuthors_ShouldReturnEmptyListOfAuthors_WhenNoAuthorsExists()
        {
            //Arrange
            
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _authorRepository.GetAllAuthors();
            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<Author>>(result);
        }

        [Fact]
        public async void GetAuthorById_ShouldReturnNull_WhenTheAuthorDoesNotExists()
        {
            //Arrange
            int id = 1;
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _authorRepository.GetAuthorById(id);
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void GetAuthorById_ShouldReturnAuthor_WhenTheAuthorExists()
        {
            //Arrange
            int id = 1;
            Author author = new()
            {
                AuthorId = id,
                Name = "Jack",
                Age = 20,
                Password = "Aliena",
                IsAlive = false
            };
            await _context.Database.EnsureDeletedAsync();
            _context.Author.Add(author);
            await _context.SaveChangesAsync();
            //Act
            var result = await _authorRepository.GetAuthorById(id);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<Author>(result);
            Assert.Equal(id, result.AuthorId);
        }

        [Fact]
        public async void UpdateAuthor_ShouldReturnNull_WhenNoAuthorIsUpdated()
        {
            //Arrange
            int id = 1;
            Author author = new()
            {
                AuthorId = id,
                Name = "Jack",
                Age = 20,
                Password = "Aliena",
                IsAlive = false
            };
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _authorRepository.UpdateAuthor(id, author);
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateAuthor_ShouldReturnUpdatedAuthor_WhenTheAuthorIsUpdated()
        {
            //Arrange
            int id = 1;
            Author author = new()
            {
                AuthorId = id,
                Name = "Jack",
                Age = 20,
                Password = "Aliena",
                IsAlive = false
            };
            Author newAuthor = new()
            {
                AuthorId = id,
                Name = "Merthin",
                Age = 30,
                Password = "Caris",
                IsAlive = true
            };
            await _context.Database.EnsureDeletedAsync();
            _context.Author.Add(author);
            await _context.SaveChangesAsync();
            //Act
            var result = await _authorRepository.UpdateAuthor(id, newAuthor);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<Author>(result);
            Assert.Equal(newAuthor.Name, result.Name);
            Assert.Equal(newAuthor.Age, result.Age);
            Assert.Equal(newAuthor.Password, result.Password);
            Assert.Equal(newAuthor.IsAlive, result.IsAlive);
        }

    }
}

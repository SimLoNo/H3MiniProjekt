using H3MiniProjekt.Api.Controllers;
using H3MiniProjekt.DAL.Database.Models;
using H3MiniProjekt.DAL.Repositories;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace H3MiniProjekt.Tests.Controllers
{
    public class AuthorControllerTests
    {

        private readonly AuthorController _authorController;
        private readonly Mock<IAuthorRepository> _mockRepository = new();

        public AuthorControllerTests()
        {
            _authorController = new(_mockRepository.Object);
        }

        [Fact]
        public async void GetAllAuthors_ShouldReturnStatusCode200_WhenAuthorsExists()
        {
            //Arrange
            List<Author> authors = new()
            {
                new()
                {
                    AuthorId = 1,
                    Name = "Ken Follet",
                    Age = 50,
                    Password = "Jack",
                    IsAlive = true
                },
                new()
                {
                    AuthorId = 2,
                    Name = "Ken Follet",
                    Age = 50,
                    Password = "Jack",
                    IsAlive = true
                }
            };

            _mockRepository
                .Setup(x =>  x.GetAllAuthors())
                .ReturnsAsync(authors);
            //Act
            var result = await _authorController.GetAllAuthors();
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAllAuthors_ShouldReturnStatusCode204_WhenNoAuthorsExits()
        {
            //Arrange
            List<Author> authors = new()
            {
                
            };

            _mockRepository
                .Setup(x => x.GetAllAuthors())
                .ReturnsAsync(authors);
            //Act
            var result = await _authorController.GetAllAuthors();
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAllAuthors_ShouldReturnStatusCode500_WhenAuthorsIsNull()
        {
            //Arrange

            _mockRepository
                .Setup(x => x.GetAllAuthors())
                .ReturnsAsync(() => null);
            //Act
            var result = await _authorController.GetAllAuthors();
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAllAuthors_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange

            _mockRepository
                .Setup(x => x.GetAllAuthors())
                .ReturnsAsync(() => throw new Exception("This is an exception."));
            //Act
            var result = await _authorController.GetAllAuthors();
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAuthor_ShouldReturnStatusCode200_WhenAuthorExists()
        {
            //Arrange
            Author author = new()
            {
                AuthorId = 1,
                Name = "Ken Follet",
                Age = 50,
                Password = "Jack",
                IsAlive = true
            };

            _mockRepository
                .Setup(x => x.GetAuthorById(It.IsAny<int>()))
                .ReturnsAsync(author);
            //Act
            var result = await _authorController.GetAuthor(It.IsAny<int>());
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAuthor_ShouldReturnStatusCode404_WhenAuthorDoesNotExist()
        {
            //Arrange

            _mockRepository
                .Setup(x => x.GetAuthorById(It.IsAny<int>()))
                .ReturnsAsync(() => null);
            //Act
            var result = await _authorController.GetAuthor(It.IsAny<int>());
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAuthor_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange

            _mockRepository
                .Setup(x => x.GetAuthorById(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("This is an exception."));
            //Act
            var result = await _authorController.GetAuthor(It.IsAny<int>());
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void PutAuthor_ShouldReturnStatusCode200_WhenAuthorIsUpdated()
        {
            //Arrange
            Author author = new()
            {
                AuthorId = 1,
                Name = "Ken Follet",
                Age = 50,
                Password = "Jack",
                IsAlive = true
            };
            _mockRepository
                .Setup(x => x.UpdateAuthor(It.IsAny<int>(), It.IsAny<Author>()))
                .ReturnsAsync(author);
            //Act
            var result = await _authorController.PutAuthor(author.AuthorId, author);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void PutAuthor_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange
            int id = 1;
            Author author = new()
            {
                AuthorId = id,
                Name = "Ken Follet",
                Age = 50,
                Password = "Jack",
                IsAlive = true
            };
            _mockRepository
                .Setup(x => x.UpdateAuthor(It.IsAny<int>(), It.IsAny<Author>()))
                .ReturnsAsync(() => throw new Exception("This is an exception"));
            //Act
            var result = await _authorController.PutAuthor(id, author);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void PutAuthor_ShouldReturnStatusCode400_WhenIdAndAuthorIdIsDifferent()
        {
            //Arrange
            int id = 1;
            Author author = new()
            {
                AuthorId = id + 1,
                Name = "Ken Follet",
                Age = 50,
                Password = "Jack",
                IsAlive = true
            };
            //Act
            var result = await _authorController.PutAuthor(id, author);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(400, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void PutAuthor_ShouldReturnStatusCode404_WhenAuthorDoesNotExist()
        {
            //Arrange
            int id = 1;
            Author author = new()
            {
                AuthorId = id,
                Name = "Ken Follet",
                Age = 50,
                Password = "Jack",
                IsAlive = true
            };


            _mockRepository
                .Setup(x => x.UpdateAuthor(It.IsAny<int>(), It.IsAny<Author>()))
                .ReturnsAsync(() => null);
            //Act
            var result = await _authorController.PutAuthor(id, author);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void PostAuthor_ShouldReturnStatusCode200_WhenAuthorIsCreated()
        {
            //Arrange
            int id = 1;
            Author author = new()
            {
                AuthorId = id,
                Name = "Ken Follet",
                Age = 50,
                Password = "Jack",
                IsAlive = true
            };


            _mockRepository
                .Setup(x => x.CreateAuthor(It.IsAny<Author>()))
                .ReturnsAsync(author);
            //Act
            var result = await _authorController.PostAuthor(author);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);

        }

        [Fact]
        public async void PostAuthor_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange
            int id = 1;
            Author author = new()
            {
                AuthorId = id,
                Name = "Ken Follet",
                Age = 50,
                Password = "Jack",
                IsAlive = true
            };


            _mockRepository
                .Setup(x => x.CreateAuthor(It.IsAny<Author>()))
                .ReturnsAsync(() => throw new Exception("This is an exception."));
            //Act
            var result = await _authorController.PostAuthor(author);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);

        }

        [Fact]
        public async void PostAuthor_ShouldReturnStatusCode404_WhenAuthorIsNotCreated()
        {
            //Arrange
            int id = 1;
            Author author = new()
            {
                AuthorId = id,
                Name = "Ken Follet",
                Age = 50,
                Password = "Jack",
                IsAlive = true
            };


            _mockRepository
                .Setup(x => x.CreateAuthor(It.IsAny<Author>()))
                .ReturnsAsync(() => null);
            //Act
            var result = await _authorController.PostAuthor(author);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);

        }

        [Fact]
        public async void DeleteAuthor_ShouldReturnStatusCode404_WhenAuthorIsNotDeleted()
        {
            //Arrange
            int id = 1;
            Author author = new()
            {
                AuthorId = id,
                Name = "Ken Follet",
                Age = 50,
                Password = "Jack",
                IsAlive = true
            };


            _mockRepository
                .Setup(x => x.DeleteAuthor(It.IsAny<int>()))
                .ReturnsAsync(() => null);
            //Act
            var result = await _authorController.DeleteAuthor(id);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void DeleteAuthor_ShouldReturnStatusCode200_WhenAuthorIsDeleted()
        {
            //Arrange
            int id = 1;
            Author author = new()
            {
                AuthorId = id,
                Name = "Ken Follet",
                Age = 50,
                Password = "Jack",
                IsAlive = true
            };


            _mockRepository
                .Setup(x => x.DeleteAuthor(It.IsAny<int>()))
                .ReturnsAsync(author);
            //Act
            var result = await _authorController.DeleteAuthor(id);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void DeleteAuthor_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange
            int id = 1;
            Author author = new()
            {
                AuthorId = id,
                Name = "Ken Follet",
                Age = 50,
                Password = "Jack",
                IsAlive = true
            };


            _mockRepository
                .Setup(x => x.DeleteAuthor(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("This is an exception."));
            //Act
            var result = await _authorController.DeleteAuthor(id);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}

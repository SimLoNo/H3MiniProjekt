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
    public class BookControllerTests
    {
        private readonly BookController _bookController;
        private readonly Mock<IBookRepository> _mockRepository = new();

        public BookControllerTests()
        {
            _bookController = new(_mockRepository.Object);
        }

        [Fact]
        public async void GetAllBooks_ShouldReturnStatusCode200_WhenBooksExists()
        {
            //Arrange
            List<Book> books = new()
            {
                new()
                {
                    BookId = 1,
                    Title = "Ken Follet",
                    Pages = 50,
                    WordCound = 600,
                    Binding = true,
                    ReleaseYear = 1990
                },
                new()
                {
                    BookId = 2,
                    Title = "Ken Follet",
                    Pages = 50,
                    WordCound = 600,
                    Binding = true,
                    ReleaseYear = 1990
                }
            };

            _mockRepository
                .Setup(x => x.GetAllBooks())
                .ReturnsAsync(books);
            //Act
            var result = await _bookController.GetAllBooks();
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAllBooks_ShouldReturnStatusCode204_WhenNoBooksExits()
        {
            //Arrange
            List<Book> books = new()
            {

            };

            _mockRepository
                .Setup(x => x.GetAllBooks())
                .ReturnsAsync(books);
            //Act
            var result = await _bookController.GetAllBooks();
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAllBooks_ShouldReturnStatusCode500_WhenBooksIsNull()
        {
            //Arrange

            _mockRepository
                .Setup(x => x.GetAllBooks())
                .ReturnsAsync(() => null);
            //Act
            var result = await _bookController.GetAllBooks();
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAllBooks_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange

            _mockRepository
                .Setup(x => x.GetAllBooks())
                .ReturnsAsync(() => throw new Exception("This is an exception."));
            //Act
            var result = await _bookController.GetAllBooks();
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetBook_ShouldReturnStatusCode200_WhenBookExists()
        {
            //Arrange
            Book book = new()
            {
                BookId = 1,
                Title = "Ken Follet",
                Pages = 50,
                WordCound = 600,
                Binding = true,
                ReleaseYear = 1990
            };

            _mockRepository
                .Setup(x => x.GetBookById(It.IsAny<int>()))
                .ReturnsAsync(book);
            //Act
            var result = await _bookController.GetBook(It.IsAny<int>());
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetBook_ShouldReturnStatusCode404_WhenBookDoesNotExist()
        {
            //Arrange

            _mockRepository
                .Setup(x => x.GetBookById(It.IsAny<int>()))
                .ReturnsAsync(() => null);
            //Act
            var result = await _bookController.GetBook(It.IsAny<int>());
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetBook_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange

            _mockRepository
                .Setup(x => x.GetBookById(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("This is an exception."));
            //Act
            var result = await _bookController.GetBook(It.IsAny<int>());
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void PutBook_ShouldReturnStatusCode200_WhenBookIsUpdated()
        {
            //Arrange
            Book book = new()
            {
                BookId = 1,
                Title = "Ken Follet",
                Pages = 50,
                WordCound = 600,
                Binding = true,
                ReleaseYear = 1990
            };
            _mockRepository
                .Setup(x => x.UpdateBook(It.IsAny<int>(), It.IsAny<Book>()))
                .ReturnsAsync(book);
            //Act
            var result = await _bookController.PutBook(book.BookId, book);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void PutBook_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange
            int id = 1;
            Book book = new()
            {
                BookId = id,
                Title = "Ken Follet",
                Pages = 50,
                WordCound = 600,
                Binding = true,
                ReleaseYear = 1990
            };
            _mockRepository
                .Setup(x => x.UpdateBook(It.IsAny<int>(), It.IsAny<Book>()))
                .ReturnsAsync(() => throw new Exception("This is an exception"));
            //Act
            var result = await _bookController.PutBook(id, book);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void PutBook_ShouldReturnStatusCode400_WhenIdAndBookIdIsDifferent()
        {
            //Arrange
            int id = 1;
            Book book = new()
            {
                BookId = id + 1,
                Title = "Ken Follet",
                Pages = 50,
                WordCound = 600,
                Binding = true,
                ReleaseYear = 1990
            };
            //Act
            var result = await _bookController.PutBook(id, book);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(400, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void PutBook_ShouldReturnStatusCode404_WhenBookDoesNotExist()
        {
            //Arrange
            int id = 1;
            Book book = new()
            {
                BookId = id,
                Title = "Ken Follet",
                Pages = 50,
                WordCound = 600,
                Binding = true,
                ReleaseYear = 1990
            };


            _mockRepository
                .Setup(x => x.UpdateBook(It.IsAny<int>(), It.IsAny<Book>()))
                .ReturnsAsync(() => null);
            //Act
            var result = await _bookController.PutBook(id, book);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void PostBook_ShouldReturnStatusCode200_WhenBookIsCreated()
        {
            //Arrange
            int id = 1;
            Book book = new()
            {
                BookId = id,
                Title = "Ken Follet",
                Pages = 50,
                WordCound = 600,
                Binding = true,
                ReleaseYear = 1990
            };


            _mockRepository
                .Setup(x => x.CreateBook(It.IsAny<Book>()))
                .ReturnsAsync(book);
            //Act
            var result = await _bookController.PostBook(book);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);

        }

        [Fact]
        public async void PostBook_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange
            int id = 1;
            Book book = new()
            {
                BookId = id,
                Title = "Ken Follet",
                Pages = 50,
                WordCound = 600,
                Binding = true,
                ReleaseYear = 1990
            };


            _mockRepository
                .Setup(x => x.CreateBook(It.IsAny<Book>()))
                .ReturnsAsync(() => throw new Exception("This is an exception."));
            //Act
            var result = await _bookController.PostBook(book);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);

        }

        [Fact]
        public async void PostBook_ShouldReturnStatusCode404_WhenBookIsNotCreated()
        {
            //Arrange
            int id = 1;
            Book book = new()
            {
                BookId = id,
                Title = "Ken Follet",
                Pages = 50,
                WordCound = 600,
                Binding = true,
                ReleaseYear = 1990
            };


            _mockRepository
                .Setup(x => x.CreateBook(It.IsAny<Book>()))
                .ReturnsAsync(() => null);
            //Act
            var result = await _bookController.PostBook(book);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);

        }

        [Fact]
        public async void DeleteBook_ShouldReturnStatusCode404_WhenBookIsNotDeleted()
        {
            //Arrange
            int id = 1;
            Book book = new()
            {
                BookId = id,
                Title = "Ken Follet",
                Pages = 50,
                WordCound = 600,
                Binding = true,
                ReleaseYear = 1990
            };


            _mockRepository
                .Setup(x => x.DeleteBook(It.IsAny<int>()))
                .ReturnsAsync(() => null);
            //Act
            var result = await _bookController.DeleteBook(id);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void DeleteBook_ShouldReturnStatusCode200_WhenBookIsDeleted()
        {
            //Arrange
            int id = 1;
            Book book = new()
            {
                BookId = id,
                Title = "Ken Follet",
                Pages = 50,
                WordCound = 600,
                Binding = true,
                ReleaseYear = 1990
            };


            _mockRepository
                .Setup(x => x.DeleteBook(It.IsAny<int>()))
                .ReturnsAsync(book);
            //Act
            var result = await _bookController.DeleteBook(id);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void DeleteBook_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange
            int id = 1;
            Book book = new()
            {
                BookId = id,
                Title = "Ken Follet",
                Pages = 50,
                WordCound = 600,
                Binding = true,
                ReleaseYear = 1990
            };


            _mockRepository
                .Setup(x => x.DeleteBook(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("This is an exception."));
            //Act
            var result = await _bookController.DeleteBook(id);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}

using System;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.BooksOperations;
using BookStore.BooksOperations.CreateBook;
using BookStore.Models;
using BookStore.DTOs;
using BookStore.UnitOfWork.Abstract;
using Moq;
using Xunit;
using System.Linq.Expressions;

public class CreateBookCommandTest
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateBookCommand _command;

    public CreateBookCommandTest()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _command = new CreateBookCommand(_mapperMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_WhenBookAlreadyExists_ThrowsInvalidOperationException()
    {
        // Arrange
        _unitOfWorkMock.Setup(u => u.BookRepository.FirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>()))
            .ReturnsAsync(new Book());

        _command.Model = new CreateBookDTO { Title = "Existing Book" };

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _command.Handle());
    }

    [Fact]
    public async Task Handle_WhenBookDoesNotExist_AddsBookAndSavesChanges()
    {
        // Arrange
        _unitOfWorkMock.Setup(u => u.BookRepository.FirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>()))
            .ReturnsAsync((Book)null);

        var newBook = new Book { Id = 1, Title = "New Book" };
        _mapperMock.Setup(m => m.Map<Book>(It.IsAny<CreateBookDTO>())).Returns(newBook);

        _command.Model = new CreateBookDTO { Title = "New Book", GenreId = 1, PageCount = 100, PublishDate = DateTime.Now };

        // Act
        await _command.Handle();

        // Assert
        _unitOfWorkMock.Verify(u => u.BookRepository.AddAsync(It.IsAny<Book>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
    }
}
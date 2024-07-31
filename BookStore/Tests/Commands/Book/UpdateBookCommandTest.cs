using System;
using System.Threading.Tasks;
using BookStore.BooksOperations;
using BookStore.DTOs;
using BookStore.Models;
using BookStore.UnitOfWork.Abstract;
using Moq;
using Xunit;

public class UpdateBookCommandTest
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly UpdateBookCommand _command;

    public UpdateBookCommandTest()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _command = new UpdateBookCommand(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_WhenBookDoesNotExist_ThrowsInvalidOperationException()
    {
        // Arrange
        _unitOfWorkMock.Setup(u => u.BookRepository.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Book)null);

        _command.BookId = 1;

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _command.Handle());
    }

    [Fact]
    public async Task Handle_WhenBookExists_UpdatesBookAndSavesChanges()
    {
        // Arrange
        var existingBook = new Book { Id = 1, Title = "Old Title", GenreId = 1 };
        _unitOfWorkMock.Setup(u => u.BookRepository.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(existingBook);

        _command.BookId = 1;
        _command.Model = new UpdateBookDTO { Title = "New Title", GenreId = 2 };

        // Act
        await _command.Handle();

        // Assert
        Assert.Equal("New Title", existingBook.Title);
        Assert.Equal(2, existingBook.GenreId);
        _unitOfWorkMock.Verify(u => u.BookRepository.UpdateAsync(existingBook), Times.Once);
        _unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
    }
}
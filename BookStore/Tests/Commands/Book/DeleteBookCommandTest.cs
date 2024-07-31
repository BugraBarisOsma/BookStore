using System;
using System.Threading.Tasks;
using BookStore.BooksOperations;
using BookStore.Models;
using BookStore.UnitOfWork.Abstract;
using Moq;
using Xunit;

public class DeleteBookCommandTest
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly DeleteBookCommand _command;

    public DeleteBookCommandTest()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _command = new DeleteBookCommand(_unitOfWorkMock.Object);
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
    public async Task Handle_WhenBookExists_DeletesBookAndSavesChanges()
    {
        // Arrange
        var existingBook = new Book { Id = 1, Title = "Existing Book" };
    
        // Mock setup for GetByIdAsync to return the existing book.
        _unitOfWorkMock.Setup(u => u.BookRepository.GetByIdAsync(It.Is<int>(id => id == 1)))
            .ReturnsAsync(existingBook);

        // Mock setup for DeleteAsync to verify it is called with correct parameters.
        _unitOfWorkMock.Setup(u => u.BookRepository.DeleteAsync(It.Is<int>(id => id == 1)))
            .Returns(Task.CompletedTask);

        _command.BookId = 1;

        // Act
        await _command.Handle();

        // Assert
        _unitOfWorkMock.Verify(u => u.BookRepository.DeleteAsync(It.Is<int>(id => id == 1)), Times.Once);
        _unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
    }
}

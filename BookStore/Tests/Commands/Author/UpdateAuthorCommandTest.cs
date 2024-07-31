using System;
using System.Threading.Tasks;
using BookStore.BooksOperations.AuthorOperations;
using BookStore.DTOs;
using BookStore.Models;
using BookStore.UnitOfWork.Abstract;
using Moq;
using Xunit;

public class UpdateAuthorCommandTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly UpdateAuthorCommand _command;

    public UpdateAuthorCommandTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _command = new UpdateAuthorCommand(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_WhenAuthorDoesNotExist_ThrowsInvalidOperationException()
    {
        // Arrange
        _unitOfWorkMock.Setup(u => u.AuthorRepository.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Author)null);

        _command.AuthorId = 1;

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _command.Handle());
    }

    [Fact]
    public async Task Handle_WhenAuthorExists_UpdatesAuthorAndSavesChanges()
    {
        // Arrange
        var existingAuthor = new Author { Id = 1, Name = "Old Name" };
        _unitOfWorkMock.Setup(u => u.AuthorRepository.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(existingAuthor);

        _command.AuthorId = 1;
        _command.Model = new UpdateAuthorDTO { Name = "New Name", Books = existingAuthor.Books };

        // Act
        await _command.Handle();

        // Assert
        Assert.Equal("New Name", existingAuthor.Name);
        _unitOfWorkMock.Verify(u => u.AuthorRepository.UpdateAsync(existingAuthor), Times.Once);
        _unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
    }
}
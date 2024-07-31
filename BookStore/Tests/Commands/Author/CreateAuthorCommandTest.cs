using System;
using System.Threading.Tasks;
using BookStore.BooksOperations.AuthorOperations;
using BookStore.DTOs;
using BookStore.Models;
using BookStore.UnitOfWork.Abstract;
using Moq;
using Xunit;

public class UpdateAuthorCommandTest
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly UpdateAuthorCommand _command;

    public UpdateAuthorCommandTest()
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

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _command.Handle());
    }

    [Fact]
    public async Task Handle_WhenAuthorExists_UpdatesAuthorAndSavesChanges()
    {
        // Arrange
        var existingAuthor = new Author { Name = "Old Name" };
        _unitOfWorkMock.Setup(u => u.AuthorRepository.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(existingAuthor);

        _command.Model = new UpdateAuthorDTO { Name = "New Name" };

        // Act
        await _command.Handle();

        // Assert
        Assert.Equal("New Name", existingAuthor.Name);
        _unitOfWorkMock.Verify(u => u.AuthorRepository.UpdateAsync(It.IsAny<Author>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
    }
}
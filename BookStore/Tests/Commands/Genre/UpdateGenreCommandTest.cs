using System;
using System.Threading.Tasks;
using BookStore.BooksOperations.GenreOperations;
using BookStore.DTOs;
using BookStore.Models;
using BookStore.UnitOfWork.Abstract;
using Moq;
using Xunit;

public class UpdateGenreCommandTest
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly UpdateGenreCommand _command;

    public UpdateGenreCommandTest()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _command = new UpdateGenreCommand(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_WhenGenreDoesNotExist_ThrowsInvalidOperationException()
    {
        // Arrange
        _unitOfWorkMock.Setup(u => u.GenreRepository.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Genre)null);

        _command.GenreId = 1;

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _command.Handle());
    }

    [Fact]
    public async Task Handle_WhenGenreExists_UpdatesGenreAndSavesChanges()
    {
        // Arrange
        var existingGenre = new Genre { Id = 1, Name = "Existing Genre", IsActive = true };
        _unitOfWorkMock.Setup(u => u.GenreRepository.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(existingGenre);

        _command.GenreId = 1;
        _command.Model = new UpdateGenreDTO { Name = "Updated Genre", IsActive = false };

        // Act
        await _command.Handle();

        // Assert
        _unitOfWorkMock.Verify(u => u.GenreRepository.UpdateAsync(It.IsAny<Genre>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
        Assert.Equal("Updated Genre", existingGenre.Name);
        Assert.False(existingGenre.IsActive);
    }
}
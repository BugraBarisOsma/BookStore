using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BookStore.BooksOperations.GenreOperations;
using BookStore.Models;
using BookStore.UnitOfWork.Abstract;
using Moq;
using Xunit;

public class DeleteGenreCommandTest
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly DeleteGenreCommand _command;

    public DeleteGenreCommandTest()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _command = new DeleteGenreCommand(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_WhenGenreDoesNotExist_ThrowsInvalidOperationException()
    {
        // Arrange
        _unitOfWorkMock.Setup(u => u.GenreRepository.FirstOrDefaultAsync(It.IsAny<Expression<Func<Genre, bool>>>()))
            .ReturnsAsync((Genre)null);

        _command.GenreId = 1;

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _command.Handle());
    }

    [Fact]
    public async Task Handle_WhenGenreExists_DeletesGenreAndSavesChanges()
    {
        // Arrange
        var genre = new Genre { Id = 1, Name = "Existing Genre" };
        _unitOfWorkMock.Setup(u => u.GenreRepository.FirstOrDefaultAsync(It.IsAny<Expression<Func<Genre, bool>>>()))
            .ReturnsAsync(genre);

        _command.GenreId = 1;

        // Act
        await _command.Handle();

        // Assert
        _unitOfWorkMock.Verify(u => u.GenreRepository.DeleteAsync(It.IsAny<int>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
    }
}
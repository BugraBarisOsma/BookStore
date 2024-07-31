using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.BooksOperations.GenreOperations;
using BookStore.DTOs;
using BookStore.Models;
using BookStore.UnitOfWork.Abstract;
using Moq;
using Xunit;

public class CreateGenreCommandTest
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateGenreCommand _command;

    public CreateGenreCommandTest()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _command = new CreateGenreCommand(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_WhenGenreAlreadyExists_ThrowsInvalidOperationException()
    {
        // Arrange
        _unitOfWorkMock.Setup(u => u.GenreRepository.FirstOrDefaultAsync(It.IsAny<Expression<Func<Genre, bool>>>()))
            .ReturnsAsync(new Genre());

        _command.Model = new CreateGenreDTO { Name = "Existing Genre" };

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _command.Handle());
    }

    [Fact]
    public async Task Handle_WhenGenreDoesNotExist_AddsGenreAndSavesChanges()
    {
        // Arrange
        _unitOfWorkMock.Setup(u => u.GenreRepository.FirstOrDefaultAsync(It.IsAny<Expression<Func<Genre, bool>>>()))
            .ReturnsAsync((Genre)null);

        var newGenre = new Genre { Id = 1, Name = "New Genre" };
        _mapperMock.Setup(m => m.Map<Genre>(It.IsAny<CreateGenreDTO>())).Returns(newGenre);

        _command.Model = new CreateGenreDTO { Name = "New Genre" };

        // Act
        await _command.Handle();

        // Assert
        _unitOfWorkMock.Verify(u => u.GenreRepository.AddAsync(It.IsAny<Genre>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
    }
}
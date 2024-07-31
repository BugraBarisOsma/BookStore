using BookStore.BooksOperations;

using BookStore.DTOs;
using BookStore.UnitOfWork.Abstract;
using BookStore.Validations;

using FluentValidation.TestHelper;
using Moq;
using Xunit;

public class UpdateBookCommandValidatorTest
{
    private readonly UpdateBookCommandValidator _validator;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    public UpdateBookCommandValidatorTest()
    {
        _validator = new UpdateBookCommandValidator();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
    }

    [Fact]
    public void Validate_WhenBookIdIsZero_ShouldHaveValidationError()
    {
        var command = new UpdateBookCommand(_mockUnitOfWork.Object) { BookId = 1 , Model = new UpdateBookDTO(){ Title = "Valid Title" , GenreId = 1 } };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.BookId);
    }

    [Fact]
    public void Validate_WhenBookIdIsGreaterThanZero_ShouldNotHaveValidationError()
    {
        var command = new UpdateBookCommand(_mockUnitOfWork.Object) { BookId = 1 , Model = new UpdateBookDTO(){ Title = "Valid Title" , GenreId = 1 } };

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(c => c.BookId);
    }

    [Fact]
    public void Validate_WhenTitleIsNullOrEmpty_ShouldHaveValidationError()
    {
        var command = new UpdateBookCommand(_mockUnitOfWork.Object) { Model = new UpdateBookDTO { Title = "" } };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Model.Title);
    }

    [Fact]
    public void Validate_WhenTitleIsNotNullOrEmpty_ShouldNotHaveValidationError()
    {
        var command = new UpdateBookCommand(_mockUnitOfWork.Object) { Model = new UpdateBookDTO { Title = "Valid Title" } };

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(c => c.Model.Title);
    }

    [Fact]
    public void Validate_WhenGenreIdIsZero_ShouldHaveValidationError()
    {
        var command = new UpdateBookCommand(_mockUnitOfWork.Object) { Model = new UpdateBookDTO { GenreId = 0 } };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Model.GenreId);
    }

    [Fact]
    public void Validate_WhenGenreIdIsGreaterThanZero_ShouldNotHaveValidationError()
    {
        var command = new UpdateBookCommand(_mockUnitOfWork.Object) { Model = new UpdateBookDTO { GenreId = 1 } };

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(c => c.Model.GenreId);
    }
}

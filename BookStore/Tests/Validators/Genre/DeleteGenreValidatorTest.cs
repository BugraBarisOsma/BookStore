using BookStore.BooksOperations.GenreOperations;
using BookStore.Validations.GenreValidations;
using FluentValidation.TestHelper;
using Xunit;

public class DeleteGenreCommandValidatorTests
{
    private readonly DeleteGenreCommandValidator _validator;

    public DeleteGenreCommandValidatorTests()
    {
        _validator = new DeleteGenreCommandValidator();
    }

    [Fact]
    public void Validate_WhenGenreIdIsZero_ShouldHaveValidationError()
    {
        // Arrange
        var command = new DeleteGenreCommand(null)
        {
            GenreId = 0
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.GenreId)
              .WithErrorMessage("Genre ID must be greater than zero.");
    }

    [Fact]
    public void Validate_WhenGenreIdIsNegative_ShouldHaveValidationError()
    {
        // Arrange
        var command = new DeleteGenreCommand(null)
        {
            GenreId = -1
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.GenreId)
              .WithErrorMessage("Genre ID must be greater than zero.");
    }

    [Fact]
    public void Validate_WhenGenreIdIsPositive_ShouldNotHaveValidationError()
    {
        // Arrange
        var command = new DeleteGenreCommand(null)
        {
            GenreId = 1
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.GenreId);
    }
}

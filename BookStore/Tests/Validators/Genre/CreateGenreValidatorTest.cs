using BookStore.BooksOperations.GenreOperations;
using BookStore.DTOs;
using BookStore.Validations.GenreValidations;
using FluentValidation.TestHelper;
using Xunit;

public class CreateGenreCommandValidatorTest
{
    private readonly CreateGenreCommandValidator _validator;

    public CreateGenreCommandValidatorTest()
    {
        _validator = new CreateGenreCommandValidator();
    }

    [Fact]
    public void Validate_WhenNameIsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateGenreCommand(null, null)
        {
            Model = new CreateGenreDTO { Name = "" }
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Model.Name)
              .WithErrorMessage("Genre name is required.");
    }

    [Fact]
    public void Validate_WhenNameIsTooShort_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateGenreCommand(null, null)
        {
            Model = new CreateGenreDTO { Name = "A" }
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Model.Name)
              .WithErrorMessage("Genre name must be at least 2 characters long.");
    }

    [Fact]
    public void Validate_WhenNameIsTooLong_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateGenreCommand(null, null)
        {
            Model = new CreateGenreDTO { Name = new string('A', 51) }
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Model.Name)
              .WithErrorMessage("Genre name must not exceed 50 characters.");
    }

    [Fact]
    public void Validate_WhenNameIsValid_ShouldNotHaveValidationError()
    {
        // Arrange
        var command = new CreateGenreCommand(null, null)
        {
            Model = new CreateGenreDTO { Name = "Valid Genre" }
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.Model.Name);
    }
}

using BookStore.BooksOperations.GenreOperations;
using BookStore.Validations.GenreValidations;
using FluentValidation.TestHelper;
using BookStore.DTOs;
using Xunit;
using Moq;
using BookStore.UnitOfWork.Abstract;

public class UpdateGenreCommandValidatorTest
{
    private readonly UpdateGenreCommandValidator _validator;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork ;

    public UpdateGenreCommandValidatorTest()
    {
        _validator = new UpdateGenreCommandValidator();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
    }

    [Fact]
    public void Validate_WhenGenreIdIsZero_ShouldHaveValidationError()
    {
        // Arrange
        var command = new UpdateGenreCommand(_mockUnitOfWork.Object)
        {
            GenreId = 0,
            Model = new UpdateGenreDTO { Name = "Genre Name", IsActive = true }
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
        var command = new UpdateGenreCommand(_mockUnitOfWork.Object)
        {
            GenreId = -1,
            Model = new UpdateGenreDTO { Name = "Genre Name", IsActive = true }
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.GenreId)
              .WithErrorMessage("Genre ID must be greater than zero.");
    }

    [Fact]
    public void Validate_WhenModelIsNull_ShouldHaveValidationError()
    {
        // Arrange
        var command = new UpdateGenreCommand(_mockUnitOfWork.Object)
        {
            GenreId = 1,
            Model = null
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Model)
              .WithErrorMessage("UpdateGenreDTO model is required.");
    }

    [Fact]
    public void Validate_WhenNameIsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var command = new UpdateGenreCommand(_mockUnitOfWork.Object)
        {
            GenreId = 1,
            Model = new UpdateGenreDTO { Name = "", IsActive = true }
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Model.Name)
              .WithErrorMessage("Genre name is required.");
    }

    [Fact]
    public void Validate_WhenNameExceedsMaxLength_ShouldHaveValidationError()
    {
        // Arrange
        var command = new UpdateGenreCommand(_mockUnitOfWork.Object)
        {
            GenreId = 1,
            Model = new UpdateGenreDTO { Name = new string('x', 51), IsActive = true }
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Model.Name)
              .WithErrorMessage("Genre name must not exceed 50 characters.");
    }

    [Fact]
    public void Validate_WhenIsActiveIsNull_ShouldHaveValidationError()
    {
        // Arrange
        var command = new UpdateGenreCommand(_mockUnitOfWork.Object)
        {
            GenreId = 1,
            Model = new UpdateGenreDTO { Name = "Genre Name", IsActive = true }
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Model.IsActive)
              .WithErrorMessage("IsActive field is required.");
    }

    [Fact]
    public void Validate_WhenAllFieldsAreValid_ShouldNotHaveValidationErrors()
    {
        // Arrange
        var command = new UpdateGenreCommand(_mockUnitOfWork.Object)
        {
            GenreId = 1,
            Model = new UpdateGenreDTO { Name = "Valid Genre Name", IsActive = true }
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.GenreId);
        result.ShouldNotHaveValidationErrorFor(c => c.Model);
        result.ShouldNotHaveValidationErrorFor(c => c.Model.Name);
        result.ShouldNotHaveValidationErrorFor(c => c.Model.IsActive);
    }
}

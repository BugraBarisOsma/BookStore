using BookStore.BooksOperations.AuthorOperations;
using BookStore.Validations.AuthorValidations;
using FluentValidation.TestHelper;
using Xunit;

public class DeleteAuthorCommandValidatorTests
{
    private readonly DeleteAuthorCommandValidator _validator;

    public DeleteAuthorCommandValidatorTests()
    {
        _validator = new DeleteAuthorCommandValidator();
    }

    [Fact]
    public void Validate_WhenAuthorIdIsLessThanOrEqualToZero_ShouldHaveValidationError()
    {
        // Arrange
        var command = new DeleteAuthorCommand(null)
        {
            AuthorId = 0
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.AuthorId)
            .WithErrorMessage("Author ID must be greater than zero.");
    }

    [Fact]
    public void Validate_WhenAuthorIdIsGreaterThanZero_ShouldNotHaveValidationError()
    {
        // Arrange
        var command = new DeleteAuthorCommand(null)
        {
            AuthorId = 1
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.AuthorId);
    }
}
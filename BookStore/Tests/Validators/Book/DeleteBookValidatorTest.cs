using BookStore.BooksOperations;
using BookStore.UnitOfWork.Abstract;
using BookStore.Validations;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

public class DeleteBookCommandValidatorTests
{
    private readonly DeleteBookCommandValidator _validator;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public DeleteBookCommandValidatorTests()
    {
        _validator = new DeleteBookCommandValidator();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
    }

    [Fact]
    public void Validate_WhenBookIdIsZero_ShouldHaveValidationError()
    {
        // Arrange
        var command = new DeleteBookCommand(_unitOfWorkMock.Object) { BookId = 0 };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.BookId);
    }

    [Fact]
    public void Validate_WhenBookIdIsGreaterThanZero_ShouldNotHaveValidationError()
    {
        // Arrange
        var command = new DeleteBookCommand(_unitOfWorkMock.Object)  { BookId = 1 };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.BookId);
    }
}
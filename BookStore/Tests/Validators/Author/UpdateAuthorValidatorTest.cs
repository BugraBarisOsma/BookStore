using BookStore.BooksOperations.AuthorOperations;
using BookStore.DTOs;
using BookStore.UnitOfWork.Abstract;
using BookStore.Validations.AuthorValidations;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

public class UpdateAuthorCommandValidatorTests
{
    private readonly UpdateAuthorCommandValidator _validator;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    public UpdateAuthorCommandValidatorTests()
    {
        _validator = new UpdateAuthorCommandValidator();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
    }

    [Fact]
    public void Validate_WhenAuthorIdIsZero_ShouldHaveValidationError()
    {
        var command = new UpdateAuthorCommand(_mockUnitOfWork.Object) { AuthorId = 0 };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.AuthorId);
    }

    [Fact]
    public void Validate_WhenAuthorIdIsGreaterThanZero_ShouldNotHaveValidationError()
    {
        var command = new UpdateAuthorCommand(_mockUnitOfWork.Object) { AuthorId = 1 };

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(c => c.AuthorId);
    }

    [Fact]
    public void Validate_WhenNameIsNullOrEmpty_ShouldHaveValidationError()
    {
        var command = new UpdateAuthorCommand(_mockUnitOfWork.Object) { Model = new UpdateAuthorDTO { Name = "" } };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Model.Name);
    }

    [Fact]
    public void Validate_WhenNameIsNotNullOrEmpty_ShouldNotHaveValidationError()
    {
        var command = new UpdateAuthorCommand(_mockUnitOfWork.Object) { Model = new UpdateAuthorDTO { Name = "Valid Name" } };

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(c => c.Model.Name);
    }
}
using AutoMapper;
using BookStore.BooksOperations.AuthorOperations;
using BookStore.DTOs;
using BookStore.UnitOfWork.Abstract;
using BookStore.Validations.AuthorValidations;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

public class CreateAuthorCommandValidatorTest
{
    private readonly CreateAuthorCommandValidator _validator;

    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    public CreateAuthorCommandValidatorTest()
    {
        _validator = new CreateAuthorCommandValidator();
        _mockMapper = new Mock<IMapper>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
    }

    [Fact]
    public void Validate_WhenModelIsNull_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateAuthorCommand(_mockUnitOfWork.Object, _mockMapper.Object)
        {
            Model = new CreateAuthorDTO { Name = "Valid Name" }
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.Model);
    }

    [Fact]
    public void Validate_WhenAuthorNameIsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateAuthorCommand(_mockUnitOfWork.Object, _mockMapper.Object)
        {
            Model = new CreateAuthorDTO { Name = string.Empty }
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Model.Name)
              .WithErrorMessage("Author name is required.");
    }

    [Fact]
    public void Validate_WhenAuthorNameExceedsMaxLength_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateAuthorCommand(_mockUnitOfWork.Object, _mockMapper.Object)
        {
            Model = new CreateAuthorDTO { Name = new string('a', 101) }
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Model.Name)
              .WithErrorMessage("Author name must not exceed 100 characters.");
    }

    [Fact]
    public void Validate_WhenAuthorNameIsValid_ShouldNotHaveValidationError()
    {
        // Arrange
        var command = new CreateAuthorCommand(_mockUnitOfWork.Object, _mockMapper.Object)
        {
            Model = new CreateAuthorDTO { Name = "Valid Author" }
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.Model.Name);
    }
}

using BookStore.BooksOperations.AuthorOperations;
using FluentValidation;

namespace BookStore.Validations.AuthorValidations;

public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(command => command.AuthorId)
            .GreaterThan(0).WithMessage("Author ID must be greater than zero.");

        RuleFor(command => command.Model)
            .NotNull().WithMessage("UpdateAuthorDTO model is required.");

        RuleFor(command => command.Model.Name)
            .NotEmpty().WithMessage("Author name cannot be empty.")
            .MaximumLength(100).WithMessage("Author name must not exceed 100 characters.");

        RuleFor(command => command.Model.Books)
            .Must(books => books != null && books.Count > 0).WithMessage("Author must have at least one book.");
    }
}
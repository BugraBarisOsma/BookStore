using BookStore.BooksOperations.AuthorOperations;
using FluentValidation;

namespace BookStore.Validations.AuthorValidations;

public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
{
    public DeleteAuthorCommandValidator()
    {
        RuleFor(command => command.AuthorId)
            .GreaterThan(0).WithMessage("Author ID must be greater than zero.");
    }
}
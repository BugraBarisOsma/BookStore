using BookStore.BooksOperations;

using FluentValidation;

namespace BookStore.Validations;

public class DeleteBookCommandValidator:AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(command => command.BookId).GreaterThan(0);
    }
}

using BookStore.BooksOperations.GenreOperations;
using FluentValidation;

namespace BookStore.Validations.GenreValidations;

public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
{
    public DeleteGenreCommandValidator()
    {
        RuleFor(command => command.GenreId)
            .GreaterThan(0).WithMessage("Genre ID must be greater than zero.");
    }
}
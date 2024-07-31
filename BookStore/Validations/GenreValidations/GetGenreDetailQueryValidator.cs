using BookStore.BooksOperations.GenreOperations;
using FluentValidation;

namespace BookStore.Validations.GenreValidations;

public class GetGenreDetailQueryValidator : AbstractValidator<GetGenreDetailQuery>
{
    public GetGenreDetailQueryValidator()
    {
        RuleFor(query => query.GenreId)
            .GreaterThan(0).WithMessage("Genre ID must be greater than zero.");
    }
}
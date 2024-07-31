
using BookStore.BooksOperations;
using FluentValidation;

namespace BookStore.Validations;

public class GetBookDetailQueryValidator:AbstractValidator<GetBookDetailQuery>
{
    public GetBookDetailQueryValidator()
    {
        RuleFor(query => query.BookId).GreaterThan(0);
    }
}

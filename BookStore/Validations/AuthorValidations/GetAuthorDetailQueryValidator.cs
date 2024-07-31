using BookStore.BooksOperations.AuthorOperations;
using FluentValidation;

namespace BookStore.Validations.AuthorValidations;

public class GetAuthorDetailQueryValidator : AbstractValidator<GetAuthorDetailQuery>
{
    public GetAuthorDetailQueryValidator()
    {
        RuleFor(query => query.AuthorId)
            .GreaterThan(0).WithMessage("Author ID must be greater than zero.");
    }
}
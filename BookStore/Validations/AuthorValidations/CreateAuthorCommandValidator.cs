using BookStore.BooksOperations.AuthorOperations;
using FluentValidation;

namespace BookStore.Validations.AuthorValidations;


    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command => command.Model)
                .NotNull().WithMessage("CreateAuthorDTO model is required.");

            RuleFor(command => command.Model.Name)
                .NotEmpty().WithMessage("Author name is required.")
                .MaximumLength(100).WithMessage("Author name must not exceed 100 characters.");


        }
    }

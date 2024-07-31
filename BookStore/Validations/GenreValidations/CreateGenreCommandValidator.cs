using BookStore.BooksOperations.GenreOperations;
using FluentValidation;

namespace BookStore.Validations.GenreValidations;

public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(command => command.Model.Name)
                .NotEmpty().WithMessage("Genre name is required.")
                .MinimumLength(2).WithMessage("Genre name must be at least 2 characters long.")
                .MaximumLength(50).WithMessage("Genre name must not exceed 50 characters.");
        }
    }


using BookStore.Context;

using BookStore.DTOs;
using BookStore.UnitOfWork.Abstract;

namespace BookStore.BooksOperations;

public class UpdateBookCommand
{
    public int BookId { get; set; }

    public UpdateBookDTO Model { get; set; }

    private readonly IUnitOfWork unitOfWork;
    public UpdateBookCommand(IUnitOfWork unitOfWork)
    {
   this.unitOfWork=unitOfWork;
    }

    public async Task Handle()
    {
        var book = await unitOfWork.BookRepository.GetByIdAsync(BookId);

        if (book is null)
        {
            throw new InvalidOperationException("There is no book with this book id.");
        }

        book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
        book.Title = Model.Title != default ? Model.Title : book.Title;

        await unitOfWork.BookRepository.UpdateAsync(book);

        await unitOfWork.Complete();

    }


}


using BookStore.Context;

using BookStore.UnitOfWork.Abstract;

namespace BookStore.BooksOperations;

public class DeleteBookCommand
{
    public int BookId { get; set; }
    private readonly IUnitOfWork unitOfWork;
    
    public DeleteBookCommand(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task Handle()
    {
        var book =await  unitOfWork.BookRepository.FirstOrDefaultAsync(x => x.Id == BookId);

        if (book is null)
        {
            throw new InvalidOperationException("There is no book with this book id.");
        }

        await unitOfWork.BookRepository.DeleteAsync(book.Id);
        await unitOfWork.Complete();
    }
}

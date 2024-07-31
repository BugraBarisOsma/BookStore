using BookStore.UnitOfWork.Abstract;

namespace BookStore.BooksOperations.AuthorOperations;

public class DeleteAuthorCommand
{
    public int AuthorId { get; set; }
    private readonly IUnitOfWork _unitOfWork;
    public DeleteAuthorCommand(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }

    public async Task Handle()
    {
        var author = await _unitOfWork.AuthorRepository.FirstOrDefaultAsync(x => x.Id == AuthorId);
        if (author is null)
        {
            throw new InvalidOperationException("There is no author with this author id.");
        }

        await _unitOfWork.AuthorRepository.DeleteAsync(author.Id);
        await _unitOfWork.Complete();
    }
}
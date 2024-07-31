using BookStore.UnitOfWork.Abstract;

namespace BookStore.BooksOperations.GenreOperations;

public class DeleteGenreCommand
{
    public int GenreId { get; set; }
    private readonly IUnitOfWork _unitOfWork;
    
    public DeleteGenreCommand(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }

    public async Task Handle()
    {
        var genre = await _unitOfWork.GenreRepository.FirstOrDefaultAsync(x => x.Id == GenreId);

        if (genre is null)
        {
            throw new InvalidOperationException("There is no genre with this genre id.");
        }

        await _unitOfWork.GenreRepository.DeleteAsync(genre.Id);
        await _unitOfWork.Complete();
    }
}
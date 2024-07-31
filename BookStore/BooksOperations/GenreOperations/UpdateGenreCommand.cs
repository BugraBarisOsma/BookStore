using BookStore.DTOs;
using BookStore.UnitOfWork.Abstract;

namespace BookStore.BooksOperations.GenreOperations;

public class UpdateGenreCommand
{
    public int GenreId { get; set; }
    public UpdateGenreDTO Model { get; set; }

    private readonly IUnitOfWork unitOfWork;
    public UpdateGenreCommand(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task Handle()
    {
        var genre = await unitOfWork.GenreRepository.GetByIdAsync(GenreId);

        if (genre is null)
        {
            throw new InvalidOperationException("There is no genre with this genre id.");
        }
        genre.Name = Model.Name != default ? Model.Name : genre.Name;
        genre.IsActive = Model.IsActive != default ? Model.IsActive : genre.IsActive;

       await  unitOfWork.GenreRepository.UpdateAsync(genre);
       await  unitOfWork.Complete();
    }
}
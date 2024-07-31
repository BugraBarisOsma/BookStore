using BookStore.DTOs;
using BookStore.UnitOfWork.Abstract;

namespace BookStore.BooksOperations.AuthorOperations;

public class UpdateAuthorCommand
{
    public int AuthorId { get; set; }
    public UpdateAuthorDTO Model { get; set; }
    
    private readonly IUnitOfWork unitOfWork;
    public UpdateAuthorCommand(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task Handle()
    {
        var author = await unitOfWork.AuthorRepository.GetByIdAsync(AuthorId);

        if (author is null)
        {
            throw new InvalidOperationException("There is no author with this author id.");
        }
        
        author.Name = Model.Name != default ? Model.Name : author.Name;
        author.Books = Model.Books != default ? Model.Books : author.Books;
        
        await unitOfWork.AuthorRepository.UpdateAsync(author);
        await unitOfWork.Complete();
    }
}
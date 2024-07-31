using AutoMapper;
using BookStore.DTOs;
using BookStore.UnitOfWork.Abstract;

namespace BookStore.BooksOperations.AuthorOperations;

public class CreateAuthorCommand
{
    public CreateAuthorDTO Model { get; set; }

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateAuthorCommand(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle()
    {
        var author = await _unitOfWork.AuthorRepository.FirstOrDefaultAsync(x => x.Name == Model.Name);
        if (author is not null)
        {
            throw new InvalidOperationException("This author already exists!");
        }

        await _unitOfWork.AuthorRepository.AddAsync(_mapper.Map<Models.Author>(Model));
        await  _unitOfWork.Complete();
    }
}
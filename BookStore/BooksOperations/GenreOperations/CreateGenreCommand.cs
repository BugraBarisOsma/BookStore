using AutoMapper;
using BookStore.DTOs;
using BookStore.UnitOfWork.Abstract;

namespace BookStore.BooksOperations.GenreOperations;

public class CreateGenreCommand
{
    public CreateGenreDTO Model { get; set; }

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateGenreCommand(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle()
    {
        var genre = await _unitOfWork.GenreRepository.FirstOrDefaultAsync(x => x.Name == Model.Name);
        if (genre is not null)
        {
            throw new InvalidOperationException("This genre already exists!");
        }

        await _unitOfWork.GenreRepository.AddAsync(_mapper.Map<Models.Genre>(Model));
        await  _unitOfWork.Complete();
    }
}
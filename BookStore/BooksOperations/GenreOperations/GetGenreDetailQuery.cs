using AutoMapper;
using BookStore.DTOs;
using BookStore.UnitOfWork.Abstract;

namespace BookStore.BooksOperations.GenreOperations;

public class GetGenreDetailQuery
{
    public int GenreId { get; set; }

    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetGenreDetailQuery(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<GenreDetailDTO> Handle()
    {
        var genre = await unitOfWork.GenreRepository.GetWhereAsync(x => x.Id == GenreId);

        if (genre is null)
        {
            throw new InvalidOperationException("There is no genre with this genre id.");
        }

        GenreDetailDTO viewModel = mapper.Map<GenreDetailDTO>(genre);
        return viewModel;
        
    }
}
using AutoMapper;
using BookStore.DTOs;
using BookStore.UnitOfWork.Abstract;

namespace BookStore.BooksOperations.GenreOperations;

public class GetGenresQuery
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetGenresQuery(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<List<GetGenresDTO>> Handle()
    {
        var genreList = await unitOfWork.GenreRepository.GetAllAsync();
        List<GetGenresDTO> genres = mapper.Map<List<GetGenresDTO>>(genreList);
        return genres;
    }
    
}
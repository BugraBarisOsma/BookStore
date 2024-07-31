using AutoMapper;
using BookStore.DTOs;
using BookStore.UnitOfWork.Abstract;

namespace BookStore.BooksOperations.AuthorOperations;

public class GetAuthorsQuery
{
    
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetAuthorsQuery(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<List<GetAuthorsDTO>> Handle()
    {
        var authorList = await unitOfWork.AuthorRepository.GetAllAsync();
        List<GetAuthorsDTO> authors = mapper.Map<List<GetAuthorsDTO>>(authorList);
        return authors;
        
    }
}
using AutoMapper;
using BookStore.Context;

using BookStore.DTOs;
using BookStore.UnitOfWork.Abstract;

namespace BookStore.BooksOperations;

public class GetBooksQuery

{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetBooksQuery( IMapper mapper,IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<List<GetBooksDTO>> Handle()
    {
        var bookList = await unitOfWork.BookRepository.GetAllAsync();

        List<GetBooksDTO> books = mapper.Map<List<GetBooksDTO>>(bookList);

        return books;
    }
    

}
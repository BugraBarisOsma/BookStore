using AutoMapper;
using BookStore.Context;

using BookStore.DTOs;
using BookStore.UnitOfWork.Abstract;

namespace BookStore.BooksOperations;

public class GetBookDetailQuery
{
    public int BookId { get; set; }

    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetBookDetailQuery( IMapper mapper,IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<BookDetailDTO> Handle()
    {
        var book = await unitOfWork.BookRepository.GetWhereAsync(x => x.Id == BookId);

        if (book is null)
        {
            throw new InvalidOperationException("There is no book with this book id.");
        }
        

        return mapper.Map<BookDetailDTO>(book);


    }



}

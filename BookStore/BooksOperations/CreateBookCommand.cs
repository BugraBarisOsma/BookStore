using AutoMapper;
using BookStore.Context;
using BookStore.UnitOfWork.Concrete;
using BookStore.DTOs;
using BookStore.Models;
using BookStore.UnitOfWork.Abstract;

namespace BookStore.BooksOperations.CreateBook;

public class CreateBookCommand
{
    public CreateBookDTO Model { get; set; }


    private readonly IMapper mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBookCommand(IMapper mapper,IUnitOfWork unitOfWork)
    {
        
        this.mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle()
    {
        var book = await  _unitOfWork.BookRepository.FirstOrDefaultAsync(x=> x.Title == Model.Title);

        if (book is not null)
        {
            throw new InvalidOperationException("This book already exists!");
        }

        book = mapper.Map<Book>(Model);

        await _unitOfWork.BookRepository.AddAsync(book);
        await _unitOfWork.Complete();
    }


}

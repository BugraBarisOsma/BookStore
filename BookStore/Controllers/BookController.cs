using AutoMapper;
using BookStore.BooksOperations;
using BookStore.BooksOperations.CreateBook;
using BookStore.Context;
using BookStore.DTOs;
using BookStore.UnitOfWork.Abstract;
using BookStore.Validations;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;



namespace BookStore.Controllers;

[ApiController]
[Route("[controller]s")]
public class BookController : ControllerBase
{

    private readonly AppDbContext _context;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;

    public BookController(AppDbContext context, IMapper mapper,IUnitOfWork unitOfWork)
    {
        _context = context;
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        GetBooksQuery query =  new GetBooksQuery(mapper,unitOfWork);
        var result = await query.Handle();
        return Ok(result);
    }

    //FromRoute
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {

        BookDetailDTO result;

            GetBookDetailQuery query = new GetBookDetailQuery(mapper,unitOfWork);
            query.BookId = id;

            GetBookDetailQueryValidator validator = new();
            validator.ValidateAndThrow(query);

            result = await query.Handle();

            return Ok(result);



    }


    [HttpPost]
    public async Task<IActionResult> AddBook([FromBody] CreateBookDTO newBook)
    {

        CreateBookCommand command = new CreateBookCommand(mapper,unitOfWork) {Model=newBook};

            command.Model = newBook;
            CreateBookCommandValidator validator = new();
            ValidationResult result= validator.Validate(command);
            validator.ValidateAndThrow(command);
            await command.Handle();

            return Ok($"{newBook.Title} has saved successfully.");

        
    }

    [HttpPut("{id}")]
    public async  Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookDTO updatedBook)
    {

            UpdateBookCommand command = new UpdateBookCommand(unitOfWork);
            command.BookId = id;
            command.Model = updatedBook;

            UpdateBookCommandValidator validator= new();
            validator.ValidateAndThrow(command);

            await command.Handle();

            return Ok($"{updatedBook.Title} updated successfully");

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {

            DeleteBookCommand command = new DeleteBookCommand(unitOfWork);
            command.BookId = id;

            DeleteBookCommandValidator validator=new();
            validator.ValidateAndThrow(command);

            await command.Handle();

            return Ok("Bookd deleted successfully.");


    }



}

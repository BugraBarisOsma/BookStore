using AutoMapper;
using BookStore.BooksOperations.AuthorOperations;
using BookStore.DTOs;
using BookStore.UnitOfWork.Abstract;
using BookStore.Validations.AuthorValidations;

using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class AuthorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAuthorsQuery(_unitOfWork, _mapper);
            var result = await query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetAuthorDetailQuery(_unitOfWork, _mapper) { AuthorId = id };

            var validator = new GetAuthorDetailQueryValidator();
            await validator.ValidateAndThrowAsync(query);

            var result = await query.Handle();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorDTO newAuthor)
        {
            var command = new CreateAuthorCommand(_unitOfWork, _mapper) { Model = newAuthor };

            var validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            await command.Handle();
            return Ok($"{newAuthor.Name} has been saved successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] UpdateAuthorDTO updatedAuthor)
        {
            var command = new UpdateAuthorCommand(_unitOfWork) { AuthorId = id, Model = updatedAuthor };

            var validator = new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            await command.Handle();
            return Ok($"{updatedAuthor.Name} updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var command = new DeleteAuthorCommand(_unitOfWork) { AuthorId = id };

            var validator = new DeleteAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            await command.Handle();
            return Ok("Author deleted successfully.");
        }
    }
}

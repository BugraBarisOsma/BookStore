using BookStore.Models;
using BookStore.Repository.Abstract;

namespace BookStore.UnitOfWork.Abstract;

public interface IUnitOfWork
{
     Task Dispose();
     Task Complete();
     public IRepository<Book> BookRepository { get; } 
     public IRepository<Genre> GenreRepository { get; } 
     public IRepository<Author> AuthorRepository { get; } 
}
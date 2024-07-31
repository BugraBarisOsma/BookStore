using BookStore.Models;

namespace BookStore.DTOs;

public class AuthorDetailDTO
{
    public string Name { get; set; }
    public ICollection<Book> Books { get; set; }
}
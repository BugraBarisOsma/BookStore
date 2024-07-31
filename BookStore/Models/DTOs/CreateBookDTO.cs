namespace BookStore.DTOs;

public class CreateBookDTO
{
    public string Title { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishDate { get; set; }
    public int GenreId { get; set; }
    public int AuthorId { get; set; }
    
}
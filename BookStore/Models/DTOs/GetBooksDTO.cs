namespace BookStore.DTOs;

public class GetBooksDTO
{
    public string Title { get; set; }
    public int PageCount { get; set; }
    public string PublishDate { get; set; }
    public string Genre { get; set; }
    public string Author { get; set; }
}
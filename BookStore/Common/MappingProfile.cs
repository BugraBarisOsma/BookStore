
using AutoMapper;
using BookStore.BooksOperations;
using BookStore.DTOs;
using BookStore.Models;
using static BookStore.BooksOperations.CreateBook.CreateBookCommand;


namespace BookStore.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Book -> BookDetailDTO
            CreateMap<Book, BookDetailDTO>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.ToString("yyyy-MM-dd")));

            // Book -> GetBooksDTO
            CreateMap<Book, GetBooksDTO>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.ToString("yyyy-MM-dd")))
                .ForMember(dest=>dest.Author,opt=>opt.MapFrom(src=>src.Author.Name)).ReverseMap();

            // CreateBookDTO -> Book
            CreateMap<CreateBookDTO, Book>()
                .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.ToUniversalTime()));

            // UpdateBookDTO -> Book
            CreateMap<UpdateBookDTO, Book>()
                .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.GenreId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));

            // Book -> UpdateBookDTO
            CreateMap<Book, UpdateBookDTO>()
                .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.GenreId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));

            CreateMap<CreateGenreDTO, Genre>().ReverseMap();
            CreateMap<Genre, GenreDetailDTO>().ReverseMap();
            CreateMap<Genre, GetGenresDTO>().ReverseMap();

            CreateMap<CreateAuthorDTO, Author>().ReverseMap();
            CreateMap<Author, AuthorDetailDTO>().ReverseMap();
            CreateMap<Author, GetAuthorsDTO>().ReverseMap();
            
            
        }
    }
}

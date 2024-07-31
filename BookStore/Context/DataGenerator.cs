using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;

namespace BookStore.Context
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                // Eğer veritabanında kitaplar varsa, veri ekleme işlemi yapılmaz
                if (context.Books.Any() || context.Authors.Any() || context.Genres.Any())
                {
                    return;
                }

                // Genre ekleme
                var fantasyGenre = new Genre { Name = "Fantasy", IsActive = true };
                var sciFiGenre = new Genre { Name = "Science Fiction", IsActive = true };

                context.Genres.AddRange(fantasyGenre, sciFiGenre);

                // Author ekleme
                var tolkien = new Author { Name = "J.R.R. Tolkien" };
                var asimov = new Author { Name = "Isaac Asimov" };

                context.Authors.AddRange(tolkien, asimov);

                // Book ekleme
                var lotr = new Book
                {
                    Title = "Lord of The Rings",
                    Genre = fantasyGenre, // Bu genre'nın zaten eklenmiş olması gerekiyor
                    PageCount = 200,
                    PublishDate = DateTime.SpecifyKind(new DateTime(2001, 06, 12), DateTimeKind.Utc),
                    Author = tolkien // Bu author'ın zaten eklenmiş olması gerekiyor
                };

                var silmarillion = new Book
                {
                    Title = "Silmarillion",
                    Genre = fantasyGenre,
                    PageCount = 999,
                    PublishDate = DateTime.SpecifyKind(new DateTime(2001, 06, 12), DateTimeKind.Utc),
                    Author = tolkien
                };

                var foundation = new Book
                {
                    Title = "Foundation",
                    Genre = sciFiGenre,
                    PageCount = 255,
                    PublishDate = DateTime.SpecifyKind(new DateTime(1951, 06, 01), DateTimeKind.Utc),
                    Author = asimov
                };

                context.Books.AddRange(lotr, silmarillion, foundation);

                // Değişiklikleri kaydet
                context.SaveChanges();
            }
        }
    }
}

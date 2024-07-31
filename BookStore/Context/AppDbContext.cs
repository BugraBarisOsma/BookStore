using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Context;

public class AppDbContext : DbContext
{
 

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }


    public DbSet<Book> Books { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Author> Authors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
    
                // Author ve Book ilişkisi
                modelBuilder.Entity<Author>()
                    .HasMany(a => a.Books)
                    .WithOne(b => b.Author)
                    .HasForeignKey(b => b.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade); // Bir yazar silindiğinde tüm kitaplar da silinir
    
                // Genre ve Book ilişkisi
                modelBuilder.Entity<Genre>()
                    .HasMany(g => g.Books)
                    .WithOne(b => b.Genre)
                    .HasForeignKey(b => b.GenreId)
                    .OnDelete(DeleteBehavior.Restrict); // Bir tür silindiğinde kitaplar etkilenmez
    
                // Diğer konfigürasyonlar
                modelBuilder.Entity<Author>()
                    .Property(a => a.Name)
                    .IsRequired()
                    .HasMaxLength(100);
    
                modelBuilder.Entity<Book>()
                    .Property(b => b.Title)
                    .IsRequired()
                    .HasMaxLength(200);
    
                modelBuilder.Entity<Genre>()
                    .Property(g => g.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            }

}

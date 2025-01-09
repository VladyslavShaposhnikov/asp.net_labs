using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace aspdotnetLabs.Models;

public class AppDbContext : DbContext 
{ 
    public DbSet<BookEntity> Books { get; set; } 
    public DbSet<PublisherEntity> Publishers { get; set; }
    private string DbPath { get; set; } 
    public AppDbContext() 
    { 
        var folder = Environment.SpecialFolder.LocalApplicationData; 
        var path = Environment.GetFolderPath(folder); 
        DbPath = System.IO.Path.Join(path, "books.db"); 
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<SelectListGroup>();
        modelBuilder.Ignore<SelectListItem>();
        modelBuilder.Entity<PublisherEntity>()
            .ToTable("publishers")
            .HasData(
                new PublisherEntity()
                {
                    Id = 101,
                    Name = "WSEI",
                    Nip = "83492384",
                    Regon = "13424234",
                },
                new PublisherEntity()
                {
                    Id = 102,
                    Name = "Firma",
                    Nip = "2498534",
                    Regon = "0873439249",
                }
            );
        
        modelBuilder.Entity<BookEntity>().HasData(
            new BookEntity()
            {
                Id = 1,
                Title = "AA",
                Author = "Adam",
                Pages = 345,
                ISBN = "978-83-01-05-1",
                PublishDate = DateTime.Parse("02/06/2018"),
                PublisherId = 101,

            },
            new BookEntity()
            {
                Id = 2,
                Title = "C#",
                Author = "Ewa",
                Pages = 234,
                ISBN = "978-83-01-04-2",
                PublishDate = DateTime.Parse("02/06/2018"),
                PublisherId = 102,
            }
        );
        
        modelBuilder.Entity<PublisherEntity>()
            .OwnsOne(e => e.Address)
            .HasData(
                new { PublisherEntityId = 101, City = "Kraków", Street = "Św. Filipa 17", PostalCode = "31-150", Region = "małopolskie" },
                new { PublisherEntityId = 102, City = "Kraków", Street = "Krowoderska 45/6", PostalCode = "31-150", Region = "małopolskie" }
            );
        
        modelBuilder.Entity<BookEntity>()
            .HasOne(b => b.Publisher)
            .WithMany(p => p.Book)
            .HasForeignKey(b => b.PublisherId);
            
        // modelBuilder.Entity<BookEntity>()
        //     .HasData(
        //     new BookEntity() { Id = 1, Title = "Harry Potter 1", Author = "J.K. Roling", Pages = 629, ISBN = "978-34-76-34-7", PublishDate = new DateTime(2000,10,10), PublisherId = 1 },
        //     new BookEntity() { Id = 2, Title = "Harry Potter 2", Author = "J.K. Roling", Pages = 712, ISBN = "978-34-76-34-8", PublishDate = new DateTime(2003,10,10), PublisherId = 2}
        // );

        modelBuilder.Entity<BookEntity>()
            .Property(e => e.PublisherId)
            .HasDefaultValue(101);
        
        // modelBuilder.Entity<BookEntity>()
        //     .Property(e => e.Created)
        //     .HasDefaultValue(DateTime.Now);
    }
} 
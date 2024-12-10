
using Microsoft.EntityFrameworkCore;

namespace aspdotnetLabs.Models;

public class AppDbContext : DbContext 
{ 
    public DbSet<BookEntity> Books { get; set; } 
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
        modelBuilder.Entity<BookEntity>().HasData(
            new BookEntity() { Id = 1, Title = "Harry Potter 1", Author = "J.K. Roling", Pages = 629, ISBN = "978-34-76-34-7", PublishDate = new DateTime(2000,10,10), Publisher = "UK" },
            new BookEntity() { Id = 2, Title = "Harry Potter 2", Author = "J.K. Roling", Pages = 712, ISBN = "978-34-76-34-8", PublishDate = new DateTime(2003,10,10), Publisher = "UK" }
        );
    }
} 
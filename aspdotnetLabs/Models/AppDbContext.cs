using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace aspdotnetLabs.Models;

public class AppDbContext : IdentityDbContext<IdentityUser> 
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
        base.OnModelCreating(modelBuilder);
        
        string ADMIN_ID = Guid.NewGuid().ToString();
        string ROLE_ID = Guid.NewGuid().ToString();
        
        string USER_ID = Guid.NewGuid().ToString();
        string USER_ROLE_ID = Guid.NewGuid().ToString();

// dodanie roli administratora
        modelBuilder.Entity<IdentityRole>()
            .HasData(new IdentityRole
        {
            Name = "admin",
            NormalizedName = "ADMIN",
            Id = ROLE_ID,
            ConcurrencyStamp = ROLE_ID
        },
        new IdentityRole()
        {
            Id = USER_ROLE_ID,
            Name = "user",
            NormalizedName = "USER",
            ConcurrencyStamp = USER_ROLE_ID
        });

// utworzenie administratora jako użytkownika
        var admin = new IdentityUser()
        {
            Id = ADMIN_ID,
            Email = "adam@wsei.edu.pl",
            EmailConfirmed = true,
            UserName = "Adam",
            NormalizedUserName = "ADAM",
            NormalizedEmail = "ADAM@WSEI.EDU.PL"
        };
        
        var user = new IdentityUser()
        {
            Id = USER_ID,
            Email = "karol@wsei.edu.pl",
            NormalizedEmail = "karol@wsei.edu.pl".ToUpper(),
            UserName = "Karol",
            NormalizedUserName = "Karol".ToUpper(),
            EmailConfirmed = true
        };

// haszowanie hasła, najlepiej wykonać to poza programem i zapisać gotowy
// PasswordHash
        PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
        
        admin.PasswordHash = ph.HashPassword(admin, "1234abcd!@#$ABCD");
        user.PasswordHash = ph.HashPassword(user, "abcd@");

// zapisanie użytkownika
        modelBuilder.Entity<IdentityUser>()
            .HasData(admin, user);
        modelBuilder.Entity<IdentityUserRole<string>>()
            .HasData(
                new IdentityUserRole<string>
                {
                    RoleId = ROLE_ID,
                    UserId = admin.Id
                },
                new IdentityUserRole<string>
                {
                    RoleId = USER_ROLE_ID,
                    UserId = admin.Id
                },
                new IdentityUserRole<string>
                {
                    RoleId = USER_ROLE_ID,
                    UserId = user.Id
                }
            
            );
        
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
using aspdotnetLabs.Models;

namespace aspdotnetLabs.Mappers;

public class BookMapper
{
    public static Book FromEntity(BookEntity entity)
    {
        return new Book()
        {
            Id = entity.Id,
            Title = entity.Title,
            Author = entity.Author,
            Pages = entity.Pages,
            ISBN = entity.ISBN,
            PublishDate = entity.PublishDate,
            //Publisher = entity.Publisher,
            Category = entity.Category,
            Created = entity.Created
        };
    }

    public static BookEntity ToEntity(Book model)
    {
        return new BookEntity()
        {
            Id = model.Id,
            Title = model.Title,
            Author = model.Author,
            Pages = model.Pages,
            ISBN = model.ISBN,
            PublishDate = model.PublishDate,
            //Publisher = model.Publisher,
            Category = model.Category,
            Created = model.Created
        };
    }
}
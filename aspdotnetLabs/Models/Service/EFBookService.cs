using aspdotnetLabs.Mappers;

namespace aspdotnetLabs.Models.Service;

public class EFBookService : IBookService
{
    private AppDbContext _context;

    public EFBookService(AppDbContext context)
    {
        _context = context;
    }
    public int Add(Book book)
    {
        _context.Books.Add(BookMapper.ToEntity(book));
        _context.SaveChanges();
        return book.Id;
    }

    public void Delete(int id)
    {
        BookEntity? find = _context.Books.Find(id);
        if (find != null)
        {
            _context.Books.Remove(find);
        }
    }

    public void Update(Book book)
    {
        _context.Books.Update(BookMapper.ToEntity(book));
    }

    public List<Book> FindAll()
    {
        return _context.Books.Select(e => BookMapper.FromEntity(e)).ToList();
    }

    public Book? FindById(int id)
    {
        return BookMapper.FromEntity(_context.Books.Find(id));
    }

    public bool Contains(int id)
    {
        return _context.Books.Any(e => e.Id == id);
    }

    public List<PublisherEntity> FindAllPublishersForVieModel()
    {
        return _context.Publishers.ToList();
    }
}